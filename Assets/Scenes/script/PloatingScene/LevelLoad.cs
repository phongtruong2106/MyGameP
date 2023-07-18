using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoad : MonoBehaviour
{ 
    private LoadLevel loadLevel;
    private Playermanager playermanager;

    private gameManager1 gamemanager;
    private audioManager AudioManager;

    [SerializeField] LayerMask playermask;

    private void Awake(){
        playermanager = FindObjectOfType<Playermanager>();
        gamemanager = FindObjectOfType<gameManager1>();
        loadLevel = FindObjectOfType<LoadLevel>();
        AudioManager = FindObjectOfType<audioManager>();
    }

   private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & playermask) != 0)
        {
            gamemanager.SaveData();
            gamemanager.LoadNextLevel();
            loadLevel.SetCheckpointPosition(playermanager.GetPosition());
            if (AudioManager != null)
            {
                AudioManager.PlaySFX(AudioManager.checkpoint);
            }

        }
    }
}
