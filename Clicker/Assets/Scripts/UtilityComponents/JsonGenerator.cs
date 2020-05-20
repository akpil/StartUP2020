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

    public void GenerateCoworkerInfo()
    {
        CoworkerInfo[] infoArr = new CoworkerInfo[2];
        infoArr[0] = new CoworkerInfo();
        infoArr[0].ID = 0;
        infoArr[0].CurrentLevel = 0;
        infoArr[0].MaxLevel = 10;

        infoArr[0].CostType = eCostType.Gold;
        infoArr[0].CostBase = 100.5;
        infoArr[0].CostCurrent = 0;
        infoArr[0].CostWeight = 1.09;
        infoArr[0].CostTenWeight = 0;

        infoArr[0].PeriodBase = 10.5f;
        infoArr[0].PeriodCurrent = 0;
        infoArr[0].PeriodUpgradeAmount = .2f;
        infoArr[0].PeriodLevelStep = 15;

        infoArr[0].ValueBase = 11.7;
        infoArr[0].ValueCurrent = 0;
        infoArr[0].ValueWeight = 1.03;
        infoArr[0].ValueCalcType = eCalculationType.Exp;


        infoArr[1] = new CoworkerInfo();
        infoArr[1].ID = 10;
        infoArr[1].CurrentLevel = 0;
        infoArr[1].MaxLevel = 100;

        infoArr[1].CostType = eCostType.Ruby;
        infoArr[1].CostBase = 100.5;
        infoArr[1].CostCurrent = 0;
        infoArr[1].CostWeight = 1.09;
        infoArr[1].CostTenWeight = 0;

        infoArr[1].PeriodBase = 10.5f;
        infoArr[1].PeriodCurrent = 0;
        infoArr[1].PeriodUpgradeAmount = .2f;
        infoArr[1].PeriodLevelStep = 15;

        infoArr[1].ValueBase = 11.7;
        infoArr[1].ValueCurrent = 0;
        infoArr[1].ValueWeight = 1.03;
        infoArr[1].ValueCalcType = eCalculationType.Sum;

        string data = JsonConvert.SerializeObject(infoArr, Formatting.Indented);
        WriteFile(data, "CoworkerInfo.json");
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
