using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerStateBase
{

    public override void EnterState()
    {
        Debug.Log("Enter Run State");

        animator.SetFloat("Move", 1);

        base.EnterState();
    }


    public override void UpdateState()
    {

        if (Input.GetKeyUp(KeyCode.W))
        {
            controller.SwitchState(PlayerState.Idle);
            return;
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            controller.SwitchState(PlayerState.Magazine);
            return;
        }




        base.UpdateState();
    }

}
