using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class StoryManager : MonoBehaviour
{
    [SerializeField]
    TextAsset inkFile;
    static Story story;
    // Start is called before the first frame update
    void Start()
    {
        story = new Story(inkFile.text);
        EventManager.NextButtonPress += AdvanceStory;
    }

    void AdvanceStory()
    {   if(story!=null && story.canContinue)
        {
            string currentParagraph = story.Continue();
            EventManager.OnStoryAdvanced(currentParagraph);
        }

    }


    private void OnDisable()
    {
        EventManager.NextButtonPress -= AdvanceStory;
    }
}
