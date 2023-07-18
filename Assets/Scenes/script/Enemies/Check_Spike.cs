using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_Spike : MonoBehaviour
{

    private audioManager audiomanager;

    private void Awake() {
       audiomanager = FindObjectOfType<audioManager>();

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Spike"))
        {
             Playermanager.isGameOver = true;
            
             Destroy(transform.parent.gameObject);
             if (audiomanager != null)
                {
                    audiomanager.PlaySFX(audiomanager.death);
                }

        }
    }
}
