using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementNew : MonoBehaviour
{
    PlayerMovem controls;

    float direction = 0;
    [SerializeField] private float speed = 400;
    [SerializeField] float jumpForce = 5;

    private bool isGrounded;
    private bool isWall;
    private int numberOfJumps = 0;
    private bool isWallSliding;
    private float wallSlidingSpeed = 2f;

    [Header("Respawn Point")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;

    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private Animator animator;
    [SerializeField] private CoinManager cm;

    [SerializeField] private bool isFacingRight = true;

    private audioManager AudioManager;

    private void Awake()
    {
        controls = new PlayerMovem();
        controls.Enable();

        controls.Land.Move.performed += ctx =>
        {
            direction = ctx.ReadValue<float>();
        };

        controls.Land.Jump.performed += ctx =>
        {
            Jump();
            if (AudioManager != null)
            {
                AudioManager.PlaySFX(AudioManager.jump);
            }
        };
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheck.localScale, 0f, groundLayer) != null;
        animator.SetBool("isGrounded", isGrounded);

        isWall = Physics2D.OverlapBox(wallCheck.position, wallCheck.localScale, 0f, groundLayer) != null;

        if (isWall && !isGrounded && playerRB.velocity.y < 0)
        {
            isWallSliding = true;
            playerRB.velocity = new Vector2(playerRB.velocity.x, -wallSlidingSpeed);
        }
        else
        {
            isWallSliding = false;
        }
        playerRB.velocity = new Vector2(direction * speed * Time.fixedDeltaTime, playerRB.velocity.y);
        animator.SetFloat("speed", Mathf.Abs(direction));

        if (isFacingRight && direction < 0 || !isFacingRight && direction > 0)
        {
            Flip();
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }

    private void Jump()
    {
        if (isGrounded)
        {
            numberOfJumps = 0;
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
            numberOfJumps++;
        }
        else
        {
            if (numberOfJumps == 1)
            {
                playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
                numberOfJumps++;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            cm.coinCount++;
        }
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

   private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Vector2 startPoint = new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheck.localScale.y / 2);
            Vector2 endPoint = new Vector2(groundCheck.position.x, groundCheck.position.y + groundCheck.localScale.y / 2);
            Gizmos.DrawLine(startPoint, endPoint);
        }

        if (wallCheck != null)
        {
            Gizmos.color = Color.blue;
            Vector2 startPoint = new Vector2(wallCheck.position.x + wallCheck.localScale.x / 2, wallCheck.position.y);
            Vector2 endPoint = new Vector2(wallCheck.position.x - wallCheck.localScale.x / 2, wallCheck.position.y);
            Gizmos.DrawLine(startPoint, endPoint);
        }
    }
}
