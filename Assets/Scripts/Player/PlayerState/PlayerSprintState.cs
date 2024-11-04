using UnityEngine;

public class PlayerSprintState : PlayerStateBase
{
    public override void EnterState()
    {
        Debug.Log("Enter Sprint State");

        animator.SetBool(PlayerAnimationNameConfig.Sprint, true);

        base.EnterState();
    }

    public override void UpdateState()
    {
        if (controller.animator.CheckAnimationName(PlayerAnimationNameConfig.Sprint) && IsAnimationOver())
        {
            controller.SwitchState(PlayerState.Idle);
        }

        base.UpdateState();
    }

    public override void ExitState()
    {
        animator.SetBool(PlayerAnimationNameConfig.Sprint, false);
        base.ExitState();
    }
}
