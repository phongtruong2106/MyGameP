using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;


public class CoinsCollectedText : MonoBehaviour
{
    [SerializeField] private int totalCoints;

    private int coinsCollected = 0;

    private TextMeshProUGUI coinsCollectedText;

    private void Awake()
    {
        coinsCollectedText = this.GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        // subscribe to events
        GameEventManager.instance.onCoinCollected += OnCoinCollected;
    }


    private void OnDestroy()
    {
        // unsubscribe from events
        GameEventManager.instance.onCoinCollected -= OnCoinCollected;
    }



    private void OnCoinCollected()
    {
        coinsCollected++;
    }

    private void Update()
    {
        coinsCollectedText.text = coinsCollected + " / " + totalCoints;
    }
}
