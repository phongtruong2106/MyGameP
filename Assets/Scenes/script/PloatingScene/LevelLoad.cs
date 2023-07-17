using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoad : MonoBehaviour
{ 
    
    // //load level cense
    //  public int ilevelTotal;

    //  public string sLevelTotal;

    // public bool useIntegerToLoadLevel = false;


    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     GameObject collisionGameObject = collision.gameObject;


    //     if(collisionGameObject.gameObject.tag == "Player")
    //     {
    //         LoadScene();
    //     }
    // }
    // private void LoadScene()
    // {
    //     if (useIntegerToLoadLevel)
    //     {
    //         SceneManager.LoadScene(ilevelTotal);
    //     }
    //     else
    //     {
    //         SceneManager.LoadScene(sLevelTotal);
    //     }
    // }

    private gameManager1 gamemanager;
    [SerializeField] LayerMask playermask;

    private void Awake(){
        gamemanager = FindObjectOfType<gameManager1>();
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(((1 << collision.gameObject.layer) & playermask) != 0){
            gamemanager.SaveData();
            gamemanager.LoadNextLevel();
        }
    }
}
