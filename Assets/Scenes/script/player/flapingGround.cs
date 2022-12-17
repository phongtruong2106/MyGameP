using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flapingGround : MonoBehaviour
{
    public float disappearingTime;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine("disappear");
        }
    }

    IEnumerator disappear()
    {
        yield return new WaitForSeconds(disappearingTime); 
        this.gameObject.SetActive(false);
    }
}
