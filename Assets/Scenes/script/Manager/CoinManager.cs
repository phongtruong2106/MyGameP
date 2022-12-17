using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public int coinCount;
    [SerializeField]private Text coinText;
    public GameObject door;
    private bool doorDestroyed;


    private void Update()
    {

        coinText.text = ":" + coinCount.ToString();

        if (coinCount == 1 && !doorDestroyed)
        {

            doorDestroyed = true;
            Destroy(door);
        }
    }
}
