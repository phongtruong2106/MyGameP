using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.Playables;

public class FileDataHandle
{

    //tao duong dan du lieu vao mot thu muc ma ta muon
    private string dataDirPath = "";

    //tao ten du lieu can duoc truyen
    private string dataFileName = "";
    private bool useEncryption = false;
    private readonly string encryptionCodeWord = "word"; //new to scene 2 //add video mul

    //tao method cong khai de lay 2 gia tri phu hop  (o day goi cho de hieu la contrustor)

    public FileDataHandle(string dataDirParh, string dataFilename, bool useEncryption)
    {
        this.dataDirPath = dataDirParh;
        this.dataFileName = dataFilename;
        this.useEncryption = useEncryption;
    }


    //tra doi tuong du lieu vao game
    public GameData1 Load(string profileId)
    {
        //base case - if the profileId is null, return right away
        if(profileId == null){
            return null;
        }

        //su dung Path.Combine tai khoan khac co os khac path 
        string fullPath = Path.Combine(dataDirPath,profileId, dataFileName);
        GameData1 loaded = null;
        //neu file tontai se tra du lieu ve
        if (File.Exists(fullPath))
        {
            //xac dinh loi 
            try
            {
                //load tiep data tu file 
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        //tao trinh doc ket thuc de tai cac tep van ban de bien du lieu de tai bien nhu mot chuoi
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                //optionally decrypt the data
                if (useEncryption)
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }
           
                //tuan tu hoa du lieu theo file json
                loaded = JsonUtility.FromJson<GameData1>(dataToLoad);
            }
            catch(Exception e)
            {
                Debug.LogError("Error occured when trying to load data  from file: " + fullPath + "\n" + e);

            }
        }

        return loaded;
    }

    //luu doi tuong du lieu cua game 
    public void Save(GameData1 data, string profileid)
    {
        //base case - if the profileid, return away right away
        if(profileid ==  null){
            return;
        }

        //tao duong dan du lieu 
        string fullPath =  Path.Combine(dataDirPath,profileid, dataFileName);

        try
        {
            //tao ra file neu file do khong ton tai
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));


            //tao mot chuoi du lieu moi trong json
            string dataStore = JsonUtility.ToJson(data, true);

            //optionally encrypt the data
            if (useEncryption)
            {
                dataStore = EncryptDecrypt(dataStore);
            }
            //write the serialized data to the file    
            using(FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using(StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataStore);
                }
            }

        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when Trying to save data to file: " + fullPath + "\n" + e);
        }
    }
    //
    public Dictionary<string, GameData1> LoadAllProfiles()
    {
        Dictionary<string, GameData1> profileDictionary = new Dictionary<string, GameData1>();

        //lap lai cac thu muc ten trong thu muc da luu tru
        //tao mot doi tuong thong tin moi
        IEnumerable<DirectoryInfo> dirInfors = new DirectoryInfo(dataDirPath).EnumerateDirectories();

        //thuc hien vong lap
        foreach(DirectoryInfo dirInfor in dirInfors)
        {
            string profileid = dirInfor.Name;
            //chuong trinh phong thu, kiem tra xem file co ton tai hay khong
            //neu no khong ton tai, thì thư mục này không phải là hồ sơ và nên bỏ qua
            string fullPath = Path.Combine(dataDirPath, profileid, dataFileName);
            if (!File.Exists(fullPath))
            {
                Debug.LogWarning("Skipping directory when doing all profiles because it does not contain data :" + profileid);
                continue;
            }

            //// tải dữ liệu trò chơi cho tệp hồ sơ này và đặt nó vào từ điển
            GameData1 profileData = Load(profileid);
            //chuong trinh bao ve - ensure the profile data isn't null;
            // because if it is the somthing went wrong and we should let ourselves know
            if(profileData != null)
            {
                profileDictionary.Add(profileid, profileData);
            }
            else
            {
                Debug.LogError("Tried to load profile but something went wrong.profile id :" + profileid);
            }

        }

        return profileDictionary;
    }

    //the below is a simple implementation of XOR encryption

    public string GetMostRecentlyUpdatedProfileID()
    {
        string mostRecentProfileID = null;
        Dictionary<string, GameData1> profilesGameData = LoadAllProfiles();
        //vong lap thong qua moi gia tri trong do key la id profile vaf value du lieu gia tri cua game 
        foreach(KeyValuePair<string, GameData1> pair in profilesGameData){
            string profileId = pair.Key;
            GameData1 gameData1 = pair.Value;

            //kiem tra xem du lieu co trong khong
            if(gameData1 == null){
                continue;
            }

            //neu tim thay data game
            if(mostRecentProfileID == null){
                mostRecentProfileID = profileId;
            }
            //otherwise , compare to see which date is the most recent
            else{
                DateTime mostRecentDateTime = DateTime.FromBinary(profilesGameData[mostRecentProfileID].lastUpdated);
                DateTime newDateTime = DateTime.FromBinary(gameData1.lastUpdated);
                //the greatest DateTime value is the most recent
                if(newDateTime > mostRecentDateTime){
                    mostRecentProfileID = profileId;
                }
            }
        }
        return mostRecentProfileID;
    }
    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";
        for(int i = 0; i < data.Length; i++)
        {
            modifiedData += (char)(data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
        }
        return modifiedData;
    }
}
