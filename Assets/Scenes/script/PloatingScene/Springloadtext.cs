using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Springloadtext : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rp;
    [SerializeField] private float jumpcace;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
          if (collision.transform.CompareTag("Player"))
        {
            anim.SetBool("onSpring1",true);
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
         if (collision.transform.CompareTag("Player"))
        {
            anim.SetBool("onSpring1", false);
        }


        if (collision.relativeVelocity.y <= 0f)
            rp = collision.collider.GetComponent<Rigidbody2D>();
        if (rp == null)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpcace, ForceMode2D.Impulse);
        }

    }

 
}
