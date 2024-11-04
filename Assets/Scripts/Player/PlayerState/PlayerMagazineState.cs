using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagazineState : PlayerStateBase
{
    private float _MagazineTime;
    private float _MagazineMaxTime;
    public override void EnterState()
    {
        Debug.Log("Enter Magazine State");
        //_MagazineMaxTime = 3f;
        //_MagazineTime = _MagazineMaxTime;

        animator.SetBool(PlayerAnimationNameConfig.Magazine, true);
        base.EnterState();
    }

    public override void UpdateState()
    {

        //Sprit
        if (Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log("failure Reload, Over Reload");
            controller.SwitchState(PlayerState.Sprint);
            return;
        }

        //Collimation
        if (Input.GetMouseButton(1))
        {
            Debug.Log("failure Reload, Over Reload");
            controller.SwitchState(PlayerState.Collimation);
            return;
        }

        #region Reloading 

        //_MagazineTime -= Time.deltaTime;
        //Debug.Log("Reload Bullet....");
        //if (_MagazineTime <= 0f)
        //{
        //    controller.SwitchState(PlayerState.Idle);
        //    controller.FinishBullet();
        //    return;
        //}

        //Whether to fire cancel back waist
        if (controller.animator.CheckAnimationName(PlayerAnimationNameConfig.Magazine))
        {
            if (CurrentAniamtionNormalizedTime > 0.75f)
            {
                if (Input.GetMouseButton(0))
                {
                    controller.SwitchState(PlayerState.Fire);
                    return;
                }
            }
        }

        if (controller.animator.CheckAnimationName(PlayerAnimationNameConfig.Magazine) && IsAnimationOver())
        {
            controller.FinishBullet();
            controller.SwitchState(PlayerState.Idle);
            return;
        }

        #endregion

        base.UpdateState();
    }

    public override void ExitState()
    {
        animator.SetBool(PlayerAnimationNameConfig.Magazine, false);
        base.ExitState();
    }

}
