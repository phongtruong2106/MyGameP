using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{

    private Rigidbody2D body;
    private Animator anim;
    private bool grouned;

   

    [SerializeField]private float _damage;


    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float speedMove;
    private BoxCollider2D boxcollider;


  

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxcollider = GetComponent<BoxCollider2D>();
        
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
#pragma warning disable UNT0002 // Inefficient tag comparison
        if (collision.gameObject.tag == "Ground")
        {
            grouned = true;
        }
#pragma warning restore UNT0002 // Inefficient tag comparison
    }

    private bool isGrounded()
    {
        RaycastHit2D rayh = Physics2D.BoxCast(boxcollider.bounds.center, boxcollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return rayh.collider != null;
    }
}
