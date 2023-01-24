using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EventManager : Singleton<EventManager>
{
    public delegate void NextButtonPressed();
    public static event NextButtonPressed NextButtonPress;
    public delegate void StoryAdvanced(string curParagraph);
    public static event StoryAdvanced StoryAdvance;
    public delegate void AnimationEnded(string animationName);
    public static event AnimationEnded AnimationEnd;



    public static void OnStoryAdvanced(string curParagraph)
    {
        if(StoryAdvance != null)
        {
            StoryAdvance(curParagraph);
            
        }
    }

    public static void OnNextButtonPressed()
    {
        if(NextButtonPress != null)
        {
            NextButtonPress();
        }
    }

    public static void OnAnimationEnded(string animationName)
    {
        if (AnimationEnd != null)
        {
            AnimationEnd(animationName);

        }
    }
}
