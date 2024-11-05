using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class CharacterMoment : MonoBehaviour
{
    [SerializeField, Header("移动速度"), Range(1, 10)]
    private float moveSpeed;
    [SerializeField, Header("旋转速度"), Range(10, 20)]
    private float rotationSpeed;
    [SerializeField, Header("冲刺速度"), Range(1, 20)]
    private float sprintSpeed;

    private float gravityAcceleration;
    private Animator animator;
    private CharacterController characterController;



    private void Awake()
    {
        gravityAcceleration = -9.8f;
        animator = GetComponentInChildren<Animator>();
        characterController = GetComponentInChildren<CharacterController>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        CharaterMove();

        UpdateGravityAcceleration();
    }

    private void FixedUpdate()
    {

    }

    private void CharaterMove()
    {
        Vector2 moveValue = CharacterInputActions.Instance.GetAxis();
        if (moveValue != Vector2.zero)
        {
            Vector3 inputMoveVec3 = new Vector3(moveValue.x, 0, moveValue.y);

            //Get main camera eulerAngles y
            float cameraAxisY = Camera.main.transform.eulerAngles.y;

            //四元数 * 向量  数学公式
            Vector3 targetDir = Quaternion.Euler(0, cameraAxisY, 0) * inputMoveVec3;

            Quaternion _targetQua = Quaternion.LookRotation(targetDir);

            float _rotationAngle = Mathf.Abs(_targetQua.eulerAngles.y - transform.eulerAngles.y);

            transform.rotation = Quaternion.Slerp(transform.rotation, _targetQua, Time.deltaTime * rotationSpeed);


            if (CharacterInputActions.Instance.IsLeftShiftPressed())
            {
                characterController.Move(transform.forward * sprintSpeed * Time.deltaTime * animator.GetFloat(PlayerAnimationNameConfig.Motion));
                animator.SetFloat(PlayerAnimationNameConfig.Motion, 1, 0.05f, Time.deltaTime);
            }
            else
            {
                characterController.Move(transform.forward * moveSpeed * Time.deltaTime * animator.GetFloat(PlayerAnimationNameConfig.Motion));
                animator.SetFloat(PlayerAnimationNameConfig.Motion, .5f, 0.05f, Time.deltaTime);
            }
        }
        else
        {
            animator.SetFloat(PlayerAnimationNameConfig.Motion, 0, 0.05f, Time.deltaTime);
        }


    }

    private void UpdateGravityAcceleration()
    {
        characterController.Move(Vector3.up * gravityAcceleration * Time.deltaTime);
    }
}
