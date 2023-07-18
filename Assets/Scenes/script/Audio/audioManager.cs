using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    public AudioSource musicSource;

     [SerializeField]
     private AudioSource SFXSource;

     public AudioClip background;
     public AudioClip jump;
     public AudioClip death;
     public AudioClip gameCompleted;
     public AudioClip menu;
     
     public AudioClip checkpoint;
    
    public static audioManager instance;

    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private void Start() {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
