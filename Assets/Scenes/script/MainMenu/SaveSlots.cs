using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlots : MonoBehaviour
{
    [Header("Profile")]
    [SerializeField] private string profileid = "";

    [Header("Content")]
    [SerializeField] private GameObject noDataContent;
    [SerializeField] private GameObject hasDataContent;
    
    [SerializeField] private TextMeshProUGUI percentageCompleteText;
    // [SerializeField] private TextMeshProUGUI deathCountText;

    //tao nut luu
    private Button saveSlotButton;


    private void Awake()
    {
        saveSlotButton = this.GetComponent<Button>();
    }


    public  void setData(GameData1 data)
    {
        //there's no data for this profileid
        if(data == null)
        {
            noDataContent.SetActive(true);
            hasDataContent.SetActive(false);
        }
        //there is data for this profileid
        else
        {
            noDataContent.SetActive(false);
            hasDataContent.SetActive(true);

            percentageCompleteText.text = data.GetPercentangeComplete() + "% COMPLETE";
            
        }
    }

    public string GetProfileID()
    {
        return this.profileid;
    }

    public void SetInteractable(bool interactable)
    {
        saveSlotButton.interactable =  interactable;
    }
}
