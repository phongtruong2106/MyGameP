using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerCollisent : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            Playermanager.isGameOver = true;
            // audioManager.instance.Play("GameOver");
            gameObject.SetActive(false);
        }
    }



}
