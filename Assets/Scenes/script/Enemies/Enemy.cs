using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float movementDistance;
    [SerializeField] private LayerMask groundLayer;

    private BoxCollider2D boxcollider;
    private PolygonCollider2D polygoncollider;
    private Animator anim;
    private bool grouned;

    //moving
    private bool movingleft;
    private float leftedge;
    private float rightEdge;


    private void Awake()
    {

        leftedge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
       
    }

    private void Update()
    {


        //duy chuyen trai
        if (movingleft)
        {
            if(transform.position.x > leftedge)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
                transform.localScale = Vector3.one;
            }
            else
            {
                movingleft = false;
            }
        }
        else
        {
            if (transform.position.x < rightEdge)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
                transform.localScale = new Vector3(-1, 1, 1);
                
            }
            else
            {
                movingleft = true;
                

            }
           
        }

    }


}
