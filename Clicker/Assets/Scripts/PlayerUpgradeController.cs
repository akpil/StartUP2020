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
    [SerializeField]
    private PlayerStatText[] mTextInfoArr;

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
            elem.Init(i, null, 
                      mTextInfoArr[i].Title,
                      mInfoArr[i].CurrentLevel.ToString(),
                      string.Format(mTextInfoArr[i].ContentsFormat,
                                    UnitSetter.GetUnitStr(mInfoArr[i].ValueCurrent),
                                    mInfoArr[i].Duration.ToString()),
                      UnitSetter.GetUnitStr(mInfoArr[i].CostCurrent),
                      LevelUP);
            mElementList.Add(elem);
        }
        
    }

    private int mSelectedID, mSelectedAmount;


    public void LevelUP(int id, int amount)
    {
        Delegates.VoidCallback callback = () => { LevelUpCallback(id, amount); };
        switch (mInfoArr[id].CostType)
        {
            case eCostType.Gold:
                {
                    GameController.Instance.GoldCallback = callback;
                    double cost = mInfoArr[id].CostCurrent *
                                        (Math.Pow(mInfoArr[id].CostWeight, amount) - 1) /
                                        (mInfoArr[id].CostWeight - 1);
                    GameController.Instance.Gold -= cost;
                }
                
                break;
            case eCostType.Ruby:
                {
                    double cost = 10 * amount;
                }
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
        mInfoArr[id].CurrentLevel += level;
        if(mInfoArr[id].CurrentLevel == mInfoArr[id].MaxLevel)
        {
            mElementList[id].SetButtonActive(false);
        }
        if(mInfoArr[id].CurrentLevel + 10 > mInfoArr[id].MaxLevel)
        {
            mElementList[id].SetTenButtonActive(false);
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
        if(mInfoArr[id].Cooltime <= 0)
        {
            switch(id)
            {
                case 0:
                    GameController.Instance.TouchPower = mInfoArr[id].ValueCurrent;
                    break;
                case 1:
                    GameController.Instance.CriticalRate = mInfoArr[id].ValueCurrent;
                    break;
                case 2:
                    GameController.Instance.CriticalValue = mInfoArr[id].ValueCurrent;
                    break;
                default:
                    Debug.LogError("wrong cooltime value on player stat " + id);
                    break;
            }
        }
        mElementList[id].Refresh(mInfoArr[id].CurrentLevel.ToString(),
                      string.Format(mTextInfoArr[id].ContentsFormat,
                                    UnitSetter.GetUnitStr(mInfoArr[id].ValueCurrent),
                                    mInfoArr[id].Duration.ToString()),
                      UnitSetter.GetUnitStr(mInfoArr[id].CostCurrent));
    }

}
