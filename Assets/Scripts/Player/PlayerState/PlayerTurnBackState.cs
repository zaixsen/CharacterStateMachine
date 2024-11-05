using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerTurnBackState : PlayerStateBase
{
    private string currentAniamtionName = string.Empty;

    public override void EnterState()
    {
        base.EnterState();

        string[] animationNames = new string[2] {
            PlayerAnimationNameConfig.Reface_Start_F_L_180 ,
            PlayerAnimationNameConfig.Reface_Start_F_R_180 };


        currentAniamtionName = animationNames[Random.Range(0, animationNames.Length)];

        controller.animator.SetTrigger(currentAniamtionName);

    }


    public override void UpdateState()
    {
        base.UpdateState();

        switch (currentAniamtionName)
        {
            case PlayerAnimationNameConfig.Reface_Start_F_L_180:
                if (IsAnimationOver())
                {
                    controller.SwitchState(PlayerState.Walk);
                    controller.PlayerRotate(180);
                    return;
                }
                break;
            case PlayerAnimationNameConfig.Reface_Start_F_R_180:
                if (IsAnimationOver())
                {
                    controller.SwitchState(PlayerState.Walk);
                    controller.PlayerRotate(180);
                    return;
                }
                break;
        }
    }
}
