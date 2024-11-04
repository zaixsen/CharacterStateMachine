using UnityEngine;

public class PlayerCollimationState : PlayerStateBase
{

    public override void EnterState()
    {
        Debug.Log("Enter Collimation State");
        animator.SetBool(PlayerAnimationNameConfig.Collimation, true);
        base.EnterState();
    }

    public override void UpdateState()
    {
        Debug.Log("Collimation......");

        //Idle
        if (IsCollimationState)
        {
            if (Input.GetMouseButtonDown(1))
            {
                controller.SwitchState(PlayerState.Idle);
                return;
            }
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
        animator.SetBool(PlayerAnimationNameConfig.Collimation, false);
        base.ExitState();
    }
}
