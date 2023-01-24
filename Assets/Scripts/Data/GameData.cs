
using Ink.Runtime;
using Ink.UnityIntegration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    
    public string storyState;
    public string currentParagraph;

    public GameData() {
        storyState = null;
        currentParagraph= null;

    }
    

}
