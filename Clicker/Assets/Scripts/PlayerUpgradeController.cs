using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class PlayerUpgradeController : MonoBehaviour
{
    public static PlayerUpgradeController Instance;

    [SerializeField]
    private PlayerStat[] mInfoArr, test;

    private List<UIElement> mElementList;
    [SerializeField]
    private UIElement mElementPrefab;
    [SerializeField]
    private Transform mElementArea;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //mInfoArr = new PlayerStat[5];
        //mInfoArr[0] = new PlayerStat();
        //mInfoArr[0].ID = 0;
        //mInfoArr[0].CurrentLevel = 1;
        //mInfoArr[0].CostType = eCostType.Gold;
        //mInfoArr[0]

        string data = JsonConvert.SerializeObject(mInfoArr);
        Debug.Log(data);
        test = JsonConvert.DeserializeObject<PlayerStat[]>(data);

        //세이브데이터 불러오기
        mElementList = new List<UIElement>();
        for(int i= 0; i < mInfoArr.Length; i++)
        {
            UIElement elem = Instantiate(mElementPrefab, mElementArea);
            elem.Init(i, null, "test1", "1", "power up", "2", LevelUP);
            mElementList.Add(elem);
        }
        
    }

    private int mSelectedID, mSelectedAmount;


    public void LevelUP(int id, int amount)
    {
        //mSelectedID = id;
        //mSelectedAmount = amount;
        Delegates.VoidCallback callback = () => { LevelUpCallback(id, amount); };
        switch (mInfoArr[id].CostType)
        {
            case eCostType.Gold:
                GameController.Instance.GoldCallback = callback;
                GameController.Instance.Gold -= mInfoArr[id].CostCurrent;
                break;
            case eCostType.Ruby:
                break;
            case eCostType.Soul:
                break;
            default:
                Debug.LogError("wrong cost type " + mInfoArr[id].CostType);
                break;
        }
    }

    public void LevelUpCallback(int id, int level)
    {

        //int id = mSelectedID;
        //int level = mSelectedAmount;
        mInfoArr[id].CurrentLevel += level;
        if(mInfoArr[id].CurrentLevel == mInfoArr[id].MaxLevel)
        {
            // 레벨업 잠금
        }
        mInfoArr[id].CostCurrent = mInfoArr[id].CostBase *
                                Math.Pow(mInfoArr[id].CostWeight, mInfoArr[id].CurrentLevel);
        if(mInfoArr[id].IsPercent)
        {
            mInfoArr[id].ValueCurrent = mInfoArr[id].ValueBase +
                                mInfoArr[id].ValueWeight * mInfoArr[id].CurrentLevel;
        }
        else
        {
            mInfoArr[id].ValueCurrent = mInfoArr[id].ValueBase *
                                Math.Pow(mInfoArr[id].ValueWeight, mInfoArr[id].CurrentLevel);
        }

        // 계산된 값 적용 UI, GameLogic
    }

}
