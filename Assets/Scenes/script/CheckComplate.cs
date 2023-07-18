using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckComplate : MonoBehaviour
{
    private audioManager audiomanager;

    private void Awake() {
       audiomanager = FindObjectOfType<audioManager>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Complate"))
        {
           UIManager.instance.OpenPanelGameComeplate();
           if (audiomanager != null)
            {
                audiomanager.PlaySFX(audiomanager.gameCompleted);
                audioManager.instance.musicSource.Stop();
            }

        }
    }  
}
