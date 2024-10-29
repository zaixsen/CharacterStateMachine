using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Run,
    Magazine, //»»µ¯
    Sprint,   //³å´Ì
    Fire,
    Collimation, //Ãé×¼
}

public class PlayerStateBase : StateBase
{
    public Animator animator;
    protected PlayerController controller;

    protected AnimatorStateInfo stateInfo;
    public override void EnterState()
    {

    }

    public override void ExitState()
    {

    }

    public override void Init(IStateMachineOwner owner)
    {
        controller = owner as PlayerController;
        animator = controller.animator;
    }

    public bool IsCurrentAnimationOver(string animationName)
    {
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.normalizedTime >= 1 && stateInfo.IsName(animationName);
    }
    public override void UnInit()
    {

    }

    public override void UpdateState()
    {

    }
}
