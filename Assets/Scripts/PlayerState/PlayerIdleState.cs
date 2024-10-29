using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerStateBase
{

    public override void EnterState()
    {
        Debug.Log("Enter Idle State");

        animator.SetFloat("Move", 0);

        base.EnterState();
    }



    public override void UpdateState()
    {
        //Move
        if (Input.GetKeyDown(KeyCode.W))
        {
            controller.SwitchState(PlayerState.Run);
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
        if (Input.GetMouseButton(1))
        {
            controller.SwitchState(PlayerState.Collimation);
            return;
        }


        base.UpdateState();
    }



}
