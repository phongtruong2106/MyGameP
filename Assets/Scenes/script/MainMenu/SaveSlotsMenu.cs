using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SaveSlotsMenu : meNu
{
    [Header("Menu Navigation")]

    [SerializeField] private mainMenu mainmenu;

    [Header("Menu Button")]
    [SerializeField] private Button backButton;

    private SaveSlots[] saveSlots;

    private bool isLoadingGame  = false;


    private void Awake()
    {
        saveSlots = this.GetComponentsInChildren<SaveSlots>();
    }

    //tao su kien slots luu 
    public void OnSaveSlotClicked(SaveSlots saveSlots)
    {
        //disable all button
        DisableMenuButton();

        //update the selected profile id to be used for data persistence
        DataPersistenceManager.instance.ChangeSelectedProfileId(saveSlots.GetProfileID());

        if (!isLoadingGame)
        {
            //tao game moi , cai nao se khoi tao du lieu bi chan
              DataPersistenceManager.instance.NewGame();
        }

        //luu tru game toan thoi gian khi load scene
        DataPersistenceManager.instance.SaveGame();
       

        //Load the scene - which will in turn save the game because of OnSceneUnLoad() in the DataPersistenceManagere
        SceneManager.LoadSceneAsync("level1");
    }

    public void OnBackClicked()
    {
        mainmenu.ActivateMenu();
        this.DeactivateMenu();
    }

    public void ActivateMenu(bool isLoadingGame)
    {
        //set this menu to active
        this.gameObject.SetActive(true);

        //set mode 
        this.isLoadingGame = isLoadingGame;

        //load tat ca file ton tai
        Dictionary<string, GameData1> profilesGameData =DataPersistenceManager.instance.GetAllProfilesGameData();

        //lặp qua từng vị trí trong giao diện người dùng và đặt nội dung phù hợp
        GameObject firstSelected  = backButton.gameObject;
        foreach (SaveSlots saveSlot in saveSlots)
        {
            GameData1 profileData = null;
            profilesGameData.TryGetValue(saveSlot.GetProfileID(), out profileData);
            saveSlot.setData(profileData);

            if(profileData == null && isLoadingGame)
            {
                saveSlot.SetInteractable(false);
            }
            else
            {

                saveSlot.SetInteractable(true);
                if (firstSelected.Equals(backButton.gameObject))
                {
                    firstSelected = saveSlot.gameObject;
                }
            }
        }


        Button firstSelectedButton = firstSelected.GetComponent<Button>();
        //set the firts selected button
        this.SetFirstSelected(firstSelectedButton);

    }

    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }

    // co hieu hoa MenuButton
    private void DisableMenuButton()
    {
        foreach(SaveSlots saveSlots in saveSlots)
        {
            saveSlots.SetInteractable(false);
        }
        backButton.interactable = false;
    }
}
