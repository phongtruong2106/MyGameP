using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{

    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider Slider;
    public Text ProgressText;
    

    public void LoadLevels(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progess = Mathf.Clamp01(operation.progress / .9f);
            Slider.value = progess;

            ProgressText.text = progess * 100f + "%";
            yield return null;
        }
    }
}
