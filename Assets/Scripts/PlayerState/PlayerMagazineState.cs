using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagazineState : PlayerStateBase
{
    private float _MagazineTime;
    private float _MagazineMaxTime;
    private const string MagazineAnimation = "Magazine";
    public override void EnterState()
    {
        Debug.Log("Enter Magazine State");
        //_MagazineMaxTime = 3f;
        //_MagazineTime = _MagazineMaxTime;

        animator.SetBool(MagazineAnimation, true);
        base.EnterState();
    }

    public override void UpdateState()
    {
        //Fire
        if (Input.GetMouseButtonDown(0))
        {
            controller.SwitchState(PlayerState.Fire);
            return;
        }

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

        if (IsCurrentAnimationOver(MagazineAnimation))
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
        animator.SetBool(MagazineAnimation, false);
        base.ExitState();
    }

}
