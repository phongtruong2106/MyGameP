using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    private Vector3 checkpointPosition;
    private void Awake()
    {
        transform.position = checkpointPosition;
    }
    public void SetCheckpointPosition(Vector3 position)
    {
        checkpointPosition = position;
    }
}
