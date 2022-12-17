using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement1 : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator anim;
    private bool grouned;
    [SerializeField] private float speedMove;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector3(horizontalInput * speedMove, body.velocity.y);


        //flip player when moving left right
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        if (Input.GetKey(KeyCode.Space) && grouned)
            Jump();

        //set animator paraments
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grouned", grouned);
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speedMove);
        anim.SetTrigger("jump");
        grouned = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grouned = true;
            
        }
    }
}
