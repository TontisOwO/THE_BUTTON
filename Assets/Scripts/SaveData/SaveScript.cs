using UnityEngine;
using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using UnityEngine.SceneManagement;

public class SaveScript : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnApplicationQuit()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;


        File.WriteAllText(Application.dataPath + "/save.txt", currentScene.ToString());
    }
}
