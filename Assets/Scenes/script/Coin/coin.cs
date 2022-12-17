using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string cointID;

    [ContextMenu("Generate guid for id (vn)")]
    private void GenerateGuid()
    {
        cointID = System.Guid.NewGuid().ToString();
    }


    public void LoadData(GameData1 data)
    {

    }

    public void SaveData(GameData1 data1)
    {

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Playermanager.numberOfCoint++;
            PlayerPrefs.SetInt("NumberOfCoints", Playermanager.numberOfCoint);
            Destroy(gameObject);
        }
    }
}
