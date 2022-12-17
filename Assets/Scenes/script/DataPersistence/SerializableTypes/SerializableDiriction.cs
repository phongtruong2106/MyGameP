using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class SerializableDiriction<Tkey, TValue>: Dictionary<Tkey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField] private List<Tkey> keys = new List<Tkey>();
    [SerializeField] private List<TValue> values = new List<TValue>();

  //luu vao danh sach

   public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear(); 
        foreach(KeyValuePair<Tkey, TValue> pair in this)
        {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }

    public void OnAfterDeserialize()
    {
        this.Clear();
        if(keys.Count != values.Count)
        {
            Debug.LogError("Tried to deserialize a SerializableDictionary , but the amount of key(" + keys.Count + ") does not match the number of values (" + values.Count + ") which indicates that something went wrong");
        }
        for(int i = 0; i < keys.Count; i++)
        {
            this.Add(keys[i], values[i]);
        }
    }

 
}
