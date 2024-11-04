using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerStateBase
{
    private float _getMoveValue;

    public override void EnterState()
    {
        Debug.Log("Enter Idle State");

        base.EnterState();
    }

    public override void UpdateState()
    {
        SetMotionAnimation(AxisV2.x, AxisV2.y, 0f);

        //Move
        if (IsMove)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                controller.SwitchState(PlayerState.Run);
                return;
            }

            controller.SwitchState(PlayerState.Walk);
            return;
        }

        //Sprit
        if (Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log("failure Reload, Over Reload");
            controller.SwitchState(PlayerState.Sprint);
            return;
        }

        //Reload
        if (Input.GetKeyUp(KeyCode.R))
        {
            controller.SwitchState(PlayerState.Magazine);
            return;
        }

        //Fire
        if (Input.GetMouseButtonDown(0))
        {
            controller.SwitchState(PlayerState.Fire);
            return;
        }

        //Collimation
        if (!IsCollimationState && Input.GetMouseButtonDown(1))
        {
            controller.SwitchState(PlayerState.Collimation);
            return;
        }


        base.UpdateState();
    }



}
