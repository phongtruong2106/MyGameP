using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalhealthbar;
    [SerializeField] private Image currenthealthbar;


    private void Start()
    {
        totalhealthbar.fillAmount = playerHealth.currentHealth / 10;
    }

    private void Update()

    {
        //truy cap vao currentHealth de quay tro lai thanh 
        currenthealthbar.fillAmount = playerHealth.currentHealth / 10; // chia cho 10
    }

}
