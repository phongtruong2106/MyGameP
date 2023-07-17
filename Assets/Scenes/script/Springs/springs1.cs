using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class springs1 : MonoBehaviour
{

    private Animator anim;
    [SerializeField]
    private float jumpforce = 1f;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            anim.SetBool("OnSpring", true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

         if (collision.transform.CompareTag("Player"))
            {
                anim.SetBool("OnSpring", false);
            }

            if(collision.relativeVelocity.y <= 0f)
            {
                    Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
                    if(rb != null)
                    {
                        Vector2 velocity = rb.velocity;
                        velocity.y = jumpforce;
                        rb.velocity = velocity;
                    }
            }
    }
}
