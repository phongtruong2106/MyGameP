using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTowLoad : MonoBehaviour
{
    private int SceneIndex;

    private void Start()
    {
        SceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            SceneManager.LoadScene(SceneIndex - 1);
    }
}
