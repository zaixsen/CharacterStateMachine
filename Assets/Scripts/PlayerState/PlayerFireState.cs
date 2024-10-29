using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireState : PlayerStateBase
{
    private const string FireAnimaton = "Fire";

    public override void EnterState()
    {
        Debug.Log("Enter Fire State");
        animator.SetBool(FireAnimaton, true);
        base.EnterState();
    }


    public override void UpdateState()
    {
        //Idle
        if (Input.GetMouseButtonUp(0))
        {
            controller.SwitchState(PlayerState.Idle);
            return;
        }

        //Reload
        if (Input.GetKeyUp(KeyCode.R))
        {
            controller.SwitchState(PlayerState.Magazine);
            return;
        }


        base.UpdateState();
    }

    public override void ExitState()
    {
        animator.SetBool(FireAnimaton, false);
        base.ExitState();
    }

}
