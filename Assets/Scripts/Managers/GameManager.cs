using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;


    private FileDataHandler fileDataHandler;

    private GameData gameData;
    List<IDataPersistence> dataPersistenceList;



    public void NewGame()
    {
        this.gameData = new GameData();


    }
    private void OnEnable()
    {
        this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        EventManager.NextButtonPress += SaveGame;

    }
    private void OnDisable()
    {
        EventManager.NextButtonPress += SaveGame;

    }

    public void SaveGame()
    {
        if (gameData == null)
        {
            return;
        }
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceList)
        {
            dataPersistenceObj.SaveData(gameData);
        }


        fileDataHandler.Save(gameData);
    }

    public void LoadGame()
    {
        this.dataPersistenceList = FindAllDataPersistenceObjects();
        this.gameData = fileDataHandler.Load();


        if (this.gameData == null)
        {
            Debug.Log("No data was found. Initializing data to defaults.");
            return;
            
        }

        foreach (IDataPersistence dataPersistenceObj in dataPersistenceList)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    public bool HasGameData()
    {
        return gameData != null;
    }

    public void ClearData()
    {
        this.dataPersistenceList = FindAllDataPersistenceObjects();
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceList)
        {
            dataPersistenceObj.ClearData(gameData);
        }
    }
}
