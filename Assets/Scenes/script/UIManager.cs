using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
     public static UIManager instance;
     [SerializeField]
     private GameObject Game_Complate_Panel;
     [SerializeField]
     private GameObject Setting_Panel;
     [SerializeField]
     private GameObject gameOverScreen;
     [SerializeField] 
     private Text timeText;
   


     private void Awake()
     {
          if (instance == null)
          {
               instance = this;
          }
          else
          {
               Destroy(this.gameObject);
          }
     }
   private void Start() 
   {      
        Game_Complate_Panel.SetActive(false);
        Setting_Panel.SetActive(false);
        gameOverScreen.SetActive(false);
   }

   public void OpenPanelGameComeplate()
   {
          float endTime = Time.time;
          float elapsedTime = endTime - Playermanager.instance.startTime;
          timeText.text = "Elapsed Time: " + elapsedTime.ToString("F2") + " seconds";

          Game_Complate_Panel.SetActive(true);
   }
    public void Quit_btn()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void OpenSettingPanel()
    {
          Setting_Panel.SetActive(true);
    }

    public void OpengameOver()
    {
          gameOverScreen.SetActive(true);
    }

    public void Hide_Setting_panel()
    {
          Setting_Panel.SetActive(false);
    }
}
