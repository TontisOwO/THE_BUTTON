using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class SaveData
{
    public const string FILENAME_SAVEDATA = "/savedata.json";

    public static void SaveGameState()
    {
        string filePathSaveData = Application.persistentDataPath + FILENAME_SAVEDATA;
        LevelData levelData = new LevelData(SceneLoader.Instance);
        Data data = new Data(levelData);
        string txt = JsonUtility.ToJson(data);
        File.WriteAllText(filePathSaveData, contents: txt);
    }
}

[Serializable]
public class Data
{
    [SerializeField] LevelData levelData;

    public Data(LevelData levelData)
    {
        this.levelData = levelData;
    }
}

[Serializable]
public class LevelData
{
    [SerializeField] public int currentLevel = 1;

    public LevelData(SceneLoader sceneLoader)
    {
        currentLevel = sceneLoader.currentLevel;
    }
}
