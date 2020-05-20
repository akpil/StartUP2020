using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoworkerController : InformationLoader
{
    public static CoworkerController Instance;
    [SerializeField]
    private CoworkerInfo[] mInfoArr;
    [SerializeField]
    private CoworkerTextInfo[] mTextInfoArr;
    //textdata class
#pragma warning disable 0649
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

        //load elemnt icon
        //load level data

        mElementList = new List<UIElement>();
        for (int i = 0; i < 3; i++)
        {
            // mInfoArr[i]CurrentLevel = 
            mInfoArr[i].CostTenWeight = (Math.Pow(mInfoArr[i].CostWeight, 10) - 1) /
                                        (mInfoArr[i].CostWeight - 1);
            //calc

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

            element.Init(i, null,
                      mTextInfoArr[i].Title,
                      mInfoArr[i].CurrentLevel.ToString(),
                      string.Format(mTextInfoArr[i].ContentsFormat,
                                    valueStr,
                                    mInfoArr[i].PeriodCurrent.ToString()),
                      UnitSetter.GetUnitStr(mInfoArr[i].CostCurrent),
                      UnitSetter.GetUnitStr(mInfoArr[i].CostCurrent * mInfoArr[i].CostTenWeight),
                      null);

            mElementList.Add(element);

        }
    }
}
