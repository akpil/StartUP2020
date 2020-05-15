using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class JsonGenerator : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GeneratePlayerTextInfo()
    {
        PlayerStatText[] infoArr = PlayerUpgradeController.Instance.GetTextInfoArr();
        string data = JsonConvert.SerializeObject(infoArr, Formatting.Indented);
        WriteFile(data, "PlayerItemText.json");
    }

    public void GeneratePlayerItemInfo()
    {
        PlayerStat[] infoArr = new PlayerStat[3];//PlayerUpgradeController.Instance.GetInfoArr();
        infoArr[0] = new PlayerStat();
        infoArr[0].ID = 0;
        string data = JsonConvert.SerializeObject(infoArr, Formatting.Indented);
        WriteFile(data, "PlayerItem.json");
    }

    private void WriteFile(string data, string fileName)
    {
        string fileLocation = string.Concat(Application.dataPath, "/", fileName);

        StreamWriter writer = new StreamWriter(fileLocation);
        writer.Write(data);
        writer.Close();
    }
    
}
