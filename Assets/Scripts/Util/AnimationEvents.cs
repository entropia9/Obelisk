using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class AnimationEvents : MonoBehaviour
{   
    public void AnimationEnd(string animationName)
    {
        EventManager.OnAnimationEnded(animationName);
    }
}
