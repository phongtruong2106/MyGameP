using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SaveSystem : MonoBehaviour
{
    public string sceneKey = "SceneIndex", savePresentKey  = "SavePresent";

    public LoadedData LoadedData{get; private set;}

    public UnityEvent<bool> OnDataLoadedResult;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        var result = LoadData();
        OnDataLoadedResult?.Invoke(result);
    }
    
    
    public void ResetData()
    {
        PlayerPrefs.DeleteKey(sceneKey);
        PlayerPrefs.DeleteKey(savePresentKey);
        LoadedData = null;
    }

     public bool LoadData()
    {

        if (PlayerPrefs.GetInt(savePresentKey) == 1)
        {
            LoadedData = new LoadedData();
            LoadedData.sceneIndex = PlayerPrefs.GetInt(sceneKey);
            return true;
        }
        return false;

    }

    public void SaveData(int SceneIndex)
    {
        if (LoadedData == null)
            LoadedData = new LoadedData();

            LoadedData.sceneIndex = SceneIndex;
            PlayerPrefs.SetInt(sceneKey, SceneIndex);
            PlayerPrefs.SetInt(savePresentKey, 1);     
      
    }

 
}

public class LoadedData
{
    public int sceneIndex = -1;
}


