using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class StoryManager : Singleton<StoryManager>, IDataPersistence
{
    [SerializeField]
    TextAsset inkFile;
    static Story story=null;
    public string currentParagraph;
    void OnEnable()
    {   if(story == null)
        {
            CreateNewStory();
        }
        EventManager.NextButtonPress += AdvanceStory;
    }

    
    void CreateNewStory()
    {
        story = new Story(inkFile.text);
        
    }

    public void ResetStory(GameData gameData)
    {   story.ResetState();
        gameData.storyState = story.state.ToJson();
 

    }
    void AdvanceStory()
    {   if(story!=null && story.canContinue)
        {
            currentParagraph = story.Continue();
            EventManager.OnStoryAdvanced(currentParagraph);

        }

    }


    private void OnDisable()
    {
        EventManager.NextButtonPress -= AdvanceStory;
    }


    public void SaveData(GameData gameData)
    {
        gameData.storyState=story.state.ToJson();
;       gameData.currentParagraph=currentParagraph;
    }

    public void LoadData(GameData gameData)
    {

         story.state.LoadJson(gameData.storyState);
         currentParagraph=gameData.currentParagraph;

        
        
    }

    public void ClearData(GameData gameData)
    {
        story.ResetState();
        
    }
}
