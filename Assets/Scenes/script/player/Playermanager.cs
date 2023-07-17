using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;



public class Playermanager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public static bool isGameOver;
    public static int numberOfCoint;
    public static int numberOfDeath;
    public static Vector2 lastCheckPointPos = new Vector2(-3, 0);
   


    //movent
    private bool jumpPressed = false;

    public static Playermanager instance { get; private set; }

    public TextMeshProUGUI cointsTEXT;
   


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Input Manager in the scene.");
        }
        instance = this;

        isGameOver = false;
        numberOfCoint = PlayerPrefs.GetInt("NumberOfCoints", 0);
        GameObject.FindGameObjectWithTag("Player").transform.position = lastCheckPointPos;
    }

    public void MovePressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            lastCheckPointPos = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            lastCheckPointPos = context.ReadValue<Vector2>();
        }
    }

    public void JumpPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            jumpPressed = true;
        }
        else if (context.canceled)
        {
            jumpPressed = false;
        }
    }

    private void Update()
    {
        if (isGameOver)
        {
            gameOverScreen.SetActive(true);
            
        }
       // cointsTEXT.text = numberOfCoint.ToString();
    }


    public Vector2 GetMoveDirection()
    {
        return lastCheckPointPos;
    }

    // for any of the below 'Get' methods, if we're getting it then we're also using it,
    // which means we should set it to false so that it can't be used again until actually
    // pressed again.

    public bool GetJumpPressed()
    {
        bool result = jumpPressed;
        RegisterJumpPressedThisFrame();
        return result;
    }

    public void RegisterJumpPressedThisFrame()
    {
        jumpPressed = false;
    }


    //feature replay
    public void ReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
