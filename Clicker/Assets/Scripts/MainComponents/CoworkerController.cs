using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoworkerController : InformationLoader
{
    public static CoworkerController Instance;

    private CoworkerInfo[] mInfoArr;
    private CoworkerTextInfo[] mTextInfoArr;

    private int[] mLevelArr;

    //textdata class
#pragma warning disable 0649
    [SerializeField]
    private Sprite[] mIconArr;

    [SerializeField]
    private Coworker[] mCoworkerArr;

    [SerializeField]
    private UIElement mElementPrefab;
    [SerializeField]
    private Transform mElementArea;
#pragma warning restore 0649
    private List<UIElement> mElementList;
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
        LoadJson(out mInfoArr, Paths.COWORKER_INFO_TABLE);
        LoadJson(out mTextInfoArr, Paths.COWORKER_TEXT_INFO_TABLE);

        mLevelArr = GameController.Instance.GetCoworkerLevelArr();

        mElementList = new List<UIElement>();
        for (int i = 0; i < mInfoArr.Length; i++)
        {
            if(mLevelArr[i] < 0)
            { continue; }
            mInfoArr[i].CurrentLevel = mLevelArr[i];

            mInfoArr[i].CostTenWeight = (Math.Pow(mInfoArr[i].CostWeight, 10) - 1) /
                                        (mInfoArr[i].CostWeight - 1);
            CalcData(i);

            UIElement element = Instantiate(mElementPrefab, mElementArea);
            string valueStr;
            if(mInfoArr[i].ValueCalcType == eCalculationType.Exp)
            {
                valueStr = UnitSetter.GetUnitStr(mInfoArr[i].ValueCurrent);
            }
            else
            {
                valueStr = mInfoArr[i].ValueCurrent.ToString("N1");
            }

            element.Init(i, mIconArr[i],
                      mTextInfoArr[i].Title,
                      mInfoArr[i].CurrentLevel.ToString(),
                      string.Format(mTextInfoArr[i].ContentsFormat,
                                    valueStr,
                                    mInfoArr[i].PeriodCurrent.ToString()),
                      UnitSetter.GetUnitStr(mInfoArr[i].CostCurrent),
                      UnitSetter.GetUnitStr(mInfoArr[i].CostCurrent * mInfoArr[i].CostTenWeight),
                      LevelUP);

            mElementList.Add(element);

        }
    }

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
        if (mInfoArr[id].CurrentLevel == mInfoArr[id].MaxLevel)
        {
            mElementList[id].SetButtonActive(false);
        }
        if (mInfoArr[id].CurrentLevel + 10 > mInfoArr[id].MaxLevel)
        {
            mElementList[id].SetTenButtonActive(false);
        }
        mLevelArr[id] = mInfoArr[id].CurrentLevel;

        if(mInfoArr[id].CurrentLevel == 1 && id + 1 < mInfoArr.Length)
        {
            int nextID = id + 1;
            mLevelArr[nextID] = mInfoArr[nextID].CurrentLevel = 0;
            CalcData(nextID);

            UIElement element = Instantiate(mElementPrefab, mElementArea);
            string valueStrNext;
            if (mInfoArr[nextID].ValueCalcType == eCalculationType.Exp)
            {
                valueStrNext = UnitSetter.GetUnitStr(mInfoArr[nextID].ValueCurrent);
            }
            else
            {
                valueStrNext = mInfoArr[nextID].ValueCurrent.ToString("N1");
            }

            element.Init(nextID, mIconArr[nextID],
                      mTextInfoArr[nextID].Title,
                      mInfoArr[nextID].CurrentLevel.ToString(),
                      string.Format(mTextInfoArr[nextID].ContentsFormat,
                                    valueStrNext,
                                    mInfoArr[nextID].PeriodCurrent.ToString()),
                      UnitSetter.GetUnitStr(mInfoArr[nextID].CostCurrent),
                      UnitSetter.GetUnitStr(mInfoArr[nextID].CostCurrent * mInfoArr[nextID].CostTenWeight),
                      LevelUP);

            mElementList.Add(element);
        }

        CalcData(id);

        // 계산된 값 적용 UI, GameLogic

        string valueStr;
        if (mInfoArr[id].ValueCalcType == eCalculationType.Exp)
        {
            valueStr = UnitSetter.GetUnitStr(mInfoArr[id].ValueCurrent);
        }
        else
        {
            valueStr = mInfoArr[id].ValueCurrent.ToString("N1");
        }
        mElementList[id].Refresh(mInfoArr[id].CurrentLevel.ToString(),
                      string.Format(mTextInfoArr[id].ContentsFormat,
                                    valueStr,
                                    mInfoArr[id].PeriodCurrent.ToString()),
                      UnitSetter.GetUnitStr(mInfoArr[id].CostCurrent),
                      UnitSetter.GetUnitStr(mInfoArr[id].CostCurrent *
                                    mInfoArr[id].CostTenWeight));
    }

    private void CalcData(int id)
    {
        mInfoArr[id].CostCurrent = mInfoArr[id].CostBase *
                                Math.Pow(mInfoArr[id].CostWeight, mInfoArr[id].CurrentLevel);
        if (mInfoArr[id].ValueCalcType == eCalculationType.Sum)
        {
            mInfoArr[id].ValueCurrent = mInfoArr[id].ValueBase +
                                mInfoArr[id].ValueWeight * mInfoArr[id].CurrentLevel;
        }
        else
        {
            mInfoArr[id].ValueCurrent = mInfoArr[id].ValueBase *
                                Math.Pow(mInfoArr[id].ValueWeight, mInfoArr[id].CurrentLevel);
        }

        if (mInfoArr[id].CurrentLevel > 0)
        {
            switch (id)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                default:
                    Debug.LogError("wrong id value on coworker " + id);
                    break;
            }
        }
    }
}
