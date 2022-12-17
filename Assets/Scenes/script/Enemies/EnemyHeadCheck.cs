using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeadCheck : MonoBehaviour
{
    [SerializeField] private Rigidbody2D bootRb;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<playermovement>())
        {
            bootRb.velocity = new Vector2(bootRb.velocity.x, 0f);
            bootRb.AddForce(Vector2.up * 300f);
        }
    }
}
