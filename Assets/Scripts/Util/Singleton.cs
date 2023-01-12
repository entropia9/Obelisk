using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton Instance { get; private set; }

    public SoundManager soundManager { get; private set; }

    public GameManager gameManager { get; private set; }


    
    
    private void Awake()
    {
        if(Instance!=null&&Instance!=this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        soundManager = GetComponentInChildren<SoundManager>();
        gameManager = GetComponentInChildren<GameManager>();
        DontDestroyOnLoad(gameObject);

}
}
