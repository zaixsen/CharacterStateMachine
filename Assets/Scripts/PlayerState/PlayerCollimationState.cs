using UnityEngine;

public class PlayerCollimationState : PlayerStateBase
{
    private const string CollimationAnimaton = "Collimation";

    public override void EnterState()
    {
        Debug.Log("Enter Collimation State");
        animator.SetBool(CollimationAnimaton, true);
        base.EnterState();
    }

    public override void UpdateState()
    {
        Debug.Log("Collimation......");

        //Idle
        if (Input.GetKeyDown(KeyCode.T))
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
        animator.SetBool(CollimationAnimaton, false);
        base.ExitState();
    }
}
