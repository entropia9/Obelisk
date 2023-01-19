using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadSceneAsync("Main");
    }

    public void SaveGame()
    {
        
    }

    public void LoadGame()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
