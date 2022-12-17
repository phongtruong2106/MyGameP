using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{

    [SerializeField] private string id;


    [ContextMenu("Generate guid for id")]

    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if(collision.gameObject.tag == "Player")
        {
            //goi doi tuong 
            CoinCounter.coinAmount += 1; //moi lan cham vao la +1
            Destroy(gameObject);
        }
    }
}
