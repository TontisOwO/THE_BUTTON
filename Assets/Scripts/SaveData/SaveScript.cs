using UnityEngine;
using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SaveScript : MonoBehaviour
{
    [SerializeField] SceneLoader loader;
    
    void Awake()
    {
        loader = GetComponent<SceneLoader>();
    }

    
    void Update()
    {
        
    }

    public void ResetData()
    {
        File.WriteAllText(Application.dataPath + "save.txt", "Level 1");
    }

    public void SaveGame(string currentLevel)
    {
        File.WriteAllText(Application.dataPath + "/save.txt", currentLevel);
    }

    private void OnApplicationQuit()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene != "Main Menu")
        {
            File.WriteAllText(Application.dataPath + "/save.txt", currentScene);
        }
        else
        {
            File.WriteAllText(Application.dataPath + "/save.txt", loader.currentLevel);
        }
    }
}
