using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class InformationLoader : MonoBehaviour
{
    protected void LoadJson<T>(out T[] dataArr, string fileLocation)
    {
        TextAsset dataAsset = Resources.Load<TextAsset>(fileLocation);
        string data = dataAsset.text;
        if(string.IsNullOrEmpty(data))
        {
            //TODO use popup
            Debug.LogError("empty string in " + fileLocation);
        }
        dataArr = JsonConvert.DeserializeObject<T[]>(data);
    }
}
