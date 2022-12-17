using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            Playermanager.lastCheckPointPos = transform.position;
            GetComponent<SpriteRenderer>().color = Color.white; 
        }
    }
}
