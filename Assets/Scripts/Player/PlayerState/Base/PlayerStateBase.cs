using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Run,
    Magazine, //换弹
    Sprint,   //冲刺
    Fire,
    Collimation, //瞄准
    Walk,
    TurnBack,
}

public class PlayerStateBase : StateBase
{
    public Animator animator;
    protected PlayerController controller;
    protected float _animationChangeSpeed;
    protected float _rotationAngle;
    protected Quaternion _targetQua;
    private Vector2 axisV2;

    protected AnimatorStateInfo stateInfo;

    protected bool IsCollimationState { get => controller.currentState == PlayerState.Collimation; }
    protected float CurrentAniamtionNormalizedTime { get => stateInfo.normalizedTime; }
    protected Vector3 MoveValue { get => new Vector3(AxisV2.x, 0, AxisV2.y); }
    protected bool IsMove { get => AxisV2 != Vector2.zero; }
    protected Vector2 AxisV2
    {
        get
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
            axisV2.x = x;
            axisV2.y = y;
            return axisV2;
        }
    }
    protected float RotationAngle { get => _rotationAngle; }
    public override void EnterState()
    {
        _animationChangeSpeed = 4;
    }

    public override void ExitState() { }

    public override void Init(IStateMachineOwner owner)
    {
        controller = owner as PlayerController;
        animator = controller.animator;
    }

    public bool IsAnimationOver()
    {
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.normalizedTime >= 1;
    }

    public override void UnInit() { }

    public override void UpdateState()
    {
        if (!IsMove) return;

        Vector3 inputMoveVec3 = new Vector3(MoveValue.x, 0, MoveValue.z);

        //Get main camera eulerAngles y
        float cameraAxisY = Camera.main.transform.eulerAngles.y;

        //四元数 * 向量  数学公式  有时间研究一下
        Vector3 targetDir = Quaternion.Euler(0, cameraAxisY, 0) * inputMoveVec3;

        _targetQua = Quaternion.LookRotation(targetDir);

        _rotationAngle = Mathf.Abs(_targetQua.eulerAngles.y - controller.transform.eulerAngles.y);

    }

    protected void SetMotionAnimation(float x, float z, float movement)
    {
        controller.animator.SetFloat(PlayerAnimationNameConfig.Movement, movement, 0.05f, Time.deltaTime);
    }

    protected float GetMotionAnimation()
    {
        return controller.animator.GetFloat(PlayerAnimationNameConfig.Movement);
    }

}
