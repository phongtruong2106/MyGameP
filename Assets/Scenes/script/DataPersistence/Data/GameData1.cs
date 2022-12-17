using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]

public class GameData1
{

    public long lastUpdated;
    public int deadthCount;


    public Vector3 playerPosition;
    public SerializableDiriction<string, bool> coinsCollected;

    // alternatively, you could use an int sceneIndex
    public string currentSceneName;


    //các giá trị được xác định trong hàm tạo này sẽ là các giá trị mặc định

    //trò chơi bắt đầu khi không có dữ liệu để tải

    public GameData1()
    {
        this.deadthCount = 0;
        playerPosition = Vector3.zero;
        //cung cap cho moi coint mo id dy nhat
         coinsCollected = new SerializableDiriction<string, bool>();

        // default for starting a new game
        currentSceneName = "";

    }

    public int GetPercentangeComplete()
    {
        //figure out how many coins we've collected 
        int totalColleced = 0;
        foreach(bool collected in coinsCollected.Values)
        {
            if (collected)
            {
                totalColleced++;
            }

        }

        //ensure we don't divide by 0  when calculating the percentage
        int percentageCompleted = -1;
        if(coinsCollected.Count != 0)
        {
            percentageCompleted = (totalColleced * 100 / coinsCollected.Count); 

        }

        return percentageCompleted;
    }

}
