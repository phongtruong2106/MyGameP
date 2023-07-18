using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerCollisent : MonoBehaviour
{

    private audioManager AudioManager;

    private void Awake() {
        AudioManager = FindObjectOfType<audioManager>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            Playermanager.isGameOver = true;
            gameObject.SetActive(false);
            if (AudioManager != null)
            {
                AudioManager.PlaySFX(AudioManager.death);
            }

        }
    }



}
