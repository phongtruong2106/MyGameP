using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class springs1 : MonoBehaviour
{

    private Animator anim;
   [SerializeField] private float bounce;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetBool("springsUp", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
            anim.SetBool("springsUp", true);
        }
    }
}
