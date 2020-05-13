using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgradeController : InformationLoader
{
    public static PlayerUpgradeController Instance;

    [SerializeField]
    private PlayerStat[] mInfoArr;
    [SerializeField]
    private PlayerStatText[] mTextInfoArr;

    public PlayerStat[] GetInfoArr()
    {
        return mInfoArr;
    }

    public PlayerStatText[] GetTextInfoArr()
    {
        return mTextInfoArr;
    }

    private Sprite[] mIconArr;

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
        LoadJson(out mInfoArr, Paths.PLAYER_ITEM_TABLE);
        LoadJson(out mTextInfoArr, Paths.PLAYER_ITEM_TEXT_TABLE);        

        mIconArr = Resources.LoadAll<Sprite>(Paths.PLAYER_ITEM_ICON);

        //세이브데이터 불러오기

        for(int i = 0; i < mInfoArr.Length; i++)
        {
            mInfoArr[i].CostTenWeight = (Math.Pow(mInfoArr[i].CostWeight, 10) - 1) /
                                        (mInfoArr[i].CostWeight - 1);
        }

        mElementList = new List<UIElement>();
        for(int i= 0; i < mInfoArr.Length; i++)
        {
            UIElement elem = Instantiate(mElementPrefab, mElementArea);
            elem.Init(i, mIconArr[i],
                      mTextInfoArr[i].Title,
                      mInfoArr[i].CurrentLevel.ToString(),
                      string.Format(mTextInfoArr[i].ContentsFormat,
                                    UnitSetter.GetUnitStr(mInfoArr[i].ValueCurrent),
                                    mInfoArr[i].Duration.ToString()),
                      UnitSetter.GetUnitStr(mInfoArr[i].CostCurrent),
                      UnitSetter.GetUnitStr(mInfoArr[i].CostCurrent * mInfoArr[i].CostTenWeight),
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
                    double cost = mInfoArr[id].CostCurrent;
                    if (amount == 10)
                    {
                        cost *= mInfoArr[id].CostTenWeight;
                    }
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
                      UnitSetter.GetUnitStr(mInfoArr[id].CostCurrent),
                      UnitSetter.GetUnitStr(mInfoArr[id].CostCurrent *
                                    mInfoArr[id].CostTenWeight));
    }

}
