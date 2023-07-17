using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.Playables;


public class DataPersistenceManager : MonoBehaviour
{
    [Header("Debugging ")]

    [SerializeField] private bool disablePersistence = false;

    [SerializeField] private bool initializeDataIfNull = false;

    [SerializeField] private bool overrideSelectedProfileid = false;

    [SerializeField] private string testSelectedProfileid = "test";

    [Header("File Storage ConFig")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;


    //bien luu du dieu 
    private GameData1 _gameData1;



    private List<IDataPersistence> _dataPersistenceList;
    //bien dung de xu ly du lieu
    private FileDataHandle dataHandle; //extension

    private string selectedProfileID = "";


    //tao trinh quan ly du lieu tinh 
    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Found more than one Data Persistence Manager in the scene. Destroying the newest one .");
            Destroy(this.gameObject);
            return;

        }
        instance = this;
        //goi method dont destroy
        DontDestroyOnLoad(this.gameObject);

        if(disablePersistence){
            Debug.LogWarning("Data Persistence is Currently disabled!");
        }
        //ung dung duong dan lien tuc cho thu muc sau do laf bien ten tep
        this.dataHandle = new FileDataHandle(Application.persistentDataPath, fileName, useEncryption);

        this.selectedProfileID = dataHandle.GetMostRecentlyUpdatedProfileID();
        if(overrideSelectedProfileid){
            this.selectedProfileID = testSelectedProfileid;
            Debug.LogWarning("Overrode selected profile id with test id: " + testSelectedProfileid);
        }
    }

    //tao phuong thuc onenable va ondisable 
    private void OnEnable()
    {
        //dang ki
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        //huy dang ki
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //phuong thuc scene se duoc load

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        this._dataPersistenceList = FindAllDataPersistenceObjects();
        LoadGame();
    }


    public void ChangeSelectedProfileId(string newProfileId)
    {
        //cap nhat thong tin su dung save va load
        this.selectedProfileID = newProfileId; 
        // load game , cai nao se su dung thong tin, cap nhat du lieu game phu hop
        LoadGame();
    }

    public  void NewGame()
    {
        this._gameData1 = new GameData1();
    }

    public void LoadGame()
    {
        //return right away if data persistence if disable
        if(disablePersistence){
            return;
        }

        //tai bat ky du lieu nao cua nguoi xu ly du lieu nhung 
        this._gameData1 = dataHandle.Load(selectedProfileID);
        //neu khong co du lieu nao load , ikhong tiep tuc

        if(this._gameData1 == null && initializeDataIfNull)
        {
            NewGame();
        }

        if(this._gameData1 == null)
        {

            Debug.Log("No data was found . A new needs to game started before data can be loaded.");
            return;
        }

        foreach(IDataPersistence dataPersistence in _dataPersistenceList)
        {
            dataPersistence.LoadData(_gameData1);
        }

        //timestamp the data so we know when it was last save
        _gameData1.lastUpdated = System.DateTime.Now.ToBinary(); //tu dong hoa ngay gio thanh ma nhi phan de co the luu de dang hon


      
    }

    public void SaveGame()
    {
         //return right away if data persistence if disable
        if(disablePersistence){
            return;
        }

        //neu khong co bat ky du lieu nao duoc luu, log canh bao se duoc hien
        if(this._gameData1 == null)
        {
            Debug.LogWarning("No data was found. A new game needs to be started before data can be saved. ");
            return ;
        }
        // chuyen du lieu cho cac du lieu khac script de cap nhat
        foreach(IDataPersistence dataPersistence in _dataPersistenceList)
        {
            dataPersistence.SaveData(_gameData1);
        }

        //luu du lieu cua file su dung the data handler

        dataHandle.Save(_gameData1, selectedProfileID);

        //In the DataPersistenceManager SaveGame() method (or alternatively in another script that implements the IDataPersistence interface), save the current scene.

        // update the current scene in our data
        Scene scene = SceneManager.GetActiveScene();
        // DON'T save this for certain scenes, like our main menu scene
        if (!scene.name.Equals("MenuStart"))
        {
            _gameData1.currentSceneName = scene.name;
        }

        // save that data to a file using the data handler 
        dataHandle.Save(_gameData1, selectedProfileID);

    }

    //loai bo load neu nguoi choi thoat game
    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        //findObjectsofType  takes in an optional boolean to include inactive gameobjects

        IEnumerable<IDataPersistence> dataPersistences = FindObjectsOfType<MonoBehaviour>(true)
            .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistences);
    }

    public bool HasGameData()
    {
        return _gameData1 != null;
    }
   

    //nhan tat ca du lieu ma nguoi choi tra ve
    public Dictionary<string, GameData1> GetAllProfilesGameData()
    {
        return dataHandle.LoadAllProfiles();    
    }

    public string GetSaveSceneName(){
        if(_gameData1 == null){
            Debug.LogError("Tried to get scene name but data was null.");
            return null;
        }
        return _gameData1.currentSceneName;
    }
}
