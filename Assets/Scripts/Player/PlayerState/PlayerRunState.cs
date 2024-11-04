using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerRunState : PlayerStateBase
{
    private float _getMoveValue;

    public override void EnterState()
    {
        Debug.Log("Enter Run State");

        base.EnterState();
    }


    public override void UpdateState()
    {
        //����

        if (!IsMove)
        {
            controller.SwitchState(PlayerState.Idle);
            return;
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            controller.SwitchState(PlayerState.Magazine);
            return;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            controller.SwitchState(PlayerState.Walk);
            return;
        }

        #region Move

        SetMotionAnimation(MoveValue.x, MoveValue.z, 1.5f);
        controller.PlayerMove(GetMotionAnimation());

        #endregion

        base.UpdateState();
    }

}