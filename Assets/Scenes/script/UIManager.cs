using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject PanelSetting;

    [SerializeField]
    private GameObject PanelGameOver;

    public bool pausegame = false;

   private void Start() {
        PanelSetting.SetActive(false);
        PanelGameOver.SetActive(false);
   }

   public void OpenPanelSetting()
   {
        PanelSetting.SetActive(true);
         Time.timeScale = 0; 

         Debug.Log("Pause");
   }

   public void Hide_PanelSetting()
   {
        PanelSetting.SetActive(false);
        Time.timeScale = 1;
   }

   public void OpenPanelGameOver()
   {
        PanelSetting.SetActive(true);
   }

   public void Hide_PanelGameOver()
   {
        PanelGameOver.SetActive(false);
   }

}
