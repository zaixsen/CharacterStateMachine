using UnityEngine;

public class PlayerSprintState : PlayerStateBase
{
    private const string SprintAnimation = "Sprint";
    public override void EnterState()
    {
        Debug.Log("Enter Sprint State");

        animator.SetBool(SprintAnimation, true);

        base.EnterState();
    }

    public override void UpdateState()
    {
        if (IsCurrentAnimationOver(SprintAnimation))
        {
            controller.SwitchState(PlayerState.Idle);
        }

        base.UpdateState();
    }

    public override void ExitState()
    {
        animator.SetBool(SprintAnimation, false);
        base.ExitState();
    }
}
