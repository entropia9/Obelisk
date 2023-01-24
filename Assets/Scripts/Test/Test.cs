using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public void Load()
    {
        GameManager.Instance.LoadGame();
    }
    public void Save()
    {
        GameManager.Instance.SaveGame();
    }
}
