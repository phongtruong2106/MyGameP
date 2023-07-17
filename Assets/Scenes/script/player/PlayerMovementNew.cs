using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementNew : MonoBehaviour, IDataPersistence
{
    PlayerMovem controls;

    float direction = 0;
    [SerializeField] private float speed = 400;
    [SerializeField] float jumpForce = 5;


 
    private bool isGrounded;
    private int numberOfJumps = 0;

    [Header("Respawn Point")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private Animator animator;
   


    [SerializeField] private bool isFacingRight = true;
    [SerializeField]private CoinManager cm;

    private void Awake()
    {
        controls = new PlayerMovem();
        controls.Enable();

        controls.Land.Move.performed += ctx =>
        {
            direction = ctx.ReadValue<float>();
        };

        controls.Land.Jump.performed += ctx => Jump();
    }

   private void FixedUpdate()
    {
        isGrounded = Physics2D.BoxCast(groundCheck.position, groundCheck.localScale, 0f, Vector2.down, 0.1f, groundLayer).collider != null;
        animator.SetBool("isGrounded", isGrounded);

        playerRB.velocity = new Vector2(direction * speed * Time.fixedDeltaTime, playerRB.velocity.y);
        animator.SetFloat("speed", Mathf.Abs(direction));

        if (isFacingRight && direction < 0 || !isFacingRight && direction > 0)
            Flip();
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }

    void Jump()
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

    public void LoadData(GameData1 data)
    {
        
        this.transform.position = data.playerPosition;
    }

    public void SaveData(GameData1 data)
    {
        data.playerPosition = this.transform.position;
      
    }

    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            cm.coinCount++;
        }
    }


}
