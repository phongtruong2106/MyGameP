using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence
{
    void LoadData(GameData1 data);   


    void SaveData(GameData1 data);
}
