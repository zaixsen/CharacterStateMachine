using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerStateBase
{

    public override void EnterState()
    {
        Debug.Log("Enter Fire State");

        base.EnterState();
    }

    public override void UpdateState()
    {
        base.UpdateState();


        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            controller.SwitchState(PlayerState.Run);
            return;
        }

        if (!IsMove)
        {
            controller.SwitchState(PlayerState.Idle);
            return;
        }

        #region Move

        
        if (_rotationAngle > 177.5f && _rotationAngle < 182.5f)
        {
            controller.SwitchState(PlayerState.TurnBack);
        }
        else
        {
            controller.transform.rotation = Quaternion.Slerp(controller.transform.rotation,
                _targetQua, Time.deltaTime * controller.rotationSpeed);
        }

        SetMotionAnimation(MoveValue.x, MoveValue.z, 1f);
        controller.PlayerMove(GetMotionAnimation());

        #endregion

    }
}
