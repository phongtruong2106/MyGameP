using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoinNewVision : MonoBehaviour
{
    private SpriteRenderer visual;
    private ParticleSystem collectParticle;
    private bool collected = false;

    private void Awake() 
    {
        visual = this.GetComponentInChildren<SpriteRenderer>();
        collectParticle = this.GetComponentInChildren<ParticleSystem>();
        collectParticle.Stop();
    }

    private void OnTriggerEnter2D() 
    {
        if (!collected) 
        {
            collectParticle.Play();
            CollectCoin();
        }
    }

    private void CollectCoin() 
    {
        collected = true;
        visual.gameObject.SetActive(false);
        GameEventManager.instance.CoinCollected();
    }

}
