using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{
    private Text coinText;
    public static int coinAmount;

    private void Start()
    {
        coinText = GetComponent<Text>();
    }

    private void Update()
    {
        coinText.text = coinAmount.ToString();
    }
}
