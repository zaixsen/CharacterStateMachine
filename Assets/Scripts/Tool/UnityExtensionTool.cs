using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class UnityExtensionTool
{
    public static bool CheckAnimationName(this Animator animator, string animationName)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(animationName);
    }


}
