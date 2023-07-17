using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainMenu : meNu
{

    [Header("Menu Navigation")]
    [SerializeField] private SaveSlotsMenu saveSlotsMenu;

    [Header("Menu Buttons")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueGameButton;
    [SerializeField] private Button loadGameButton;


    private void Start()
    {
        if (!DataPersistenceManager.instance.HasGameData())
        {
            //khong co du lieu
            continueGameButton.interactable = false;
            loadGameButton.interactable = false;
        }
    }

    //Create event click button for newGAME AND cONTINUEGAME

    public void OnNewGameClicked()
    {
        saveSlotsMenu.ActivateMenu(false);
        this.DeactivateMenu();
    }

    //Create button event LoadGAME
    public void OnLoadGameClicked()
    {
        saveSlotsMenu.ActivateMenu(true);
        this.DeactivateMenu();
    }

    public void OnContinueGameClicked()
    {
        DisableMenuButton();
        //Load the next scene - which will in turn load the game because of
        DataPersistenceManager.instance.SaveGame();
        //OnsceneLoaded() trong DataPersistenceManager
        SceneManager.LoadSceneAsync(DataPersistenceManager.instance.GetSaveSceneName());
    }

    private void DisableMenuButton()
    {
        newGameButton.interactable = false;
        continueGameButton.interactable = false;
    }

    public void ActivateMenu()
    {
        this.gameObject.SetActive(true);
    }

    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }
}
