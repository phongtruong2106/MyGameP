using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CointGold : MonoBehaviour
{
    [SerializeField] private string id;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid() 
    {
        id = System.Guid.NewGuid().ToString();
    }

    private SpriteRenderer visual;
    private ParticleSystem collectParticle;
    private bool collected = false;

    private void Awake() 
    {
        visual = this.GetComponentInChildren<SpriteRenderer>();
        collectParticle = this.GetComponentInChildren<ParticleSystem>();
        collectParticle.Stop();
    }


    /* private void OnTriggerEnter2D() 
     {
         if (!collected) 
         {
             collectParticle.Play();
             CollectCoin();
         }
     }
 */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
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
