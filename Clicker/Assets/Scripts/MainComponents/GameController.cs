using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : SaveDataController
{
    public static GameController Instance;
    

    public Delegates.VoidCallback GoldCallback;
    public double Gold
    {
        get { return mUser.Gold; }
        set
        {
            if(value >= 0)
            {
                mUser.Gold = value;
                if(GoldCallback != null)
                {
                    GoldCallback();
                }
            }
            else
            {
                Debug.Log("not enough gold");
            }
            GoldCallback = null;
        }
    }

    [SerializeField]
    private double mIncomeWeight = 1.04d;
    private double mIncome;
    public double IncomeBonus { get; set; }

    [SerializeField]
    private double mProgressWeight = 1.08d;
    private double mMaxProgress;

    private double mTouchPower;
    public double TouchPower
    {
        get { return mTouchPower; }
        set
        {
            if(value >= 0)
            {
                mTouchPower = value;
            }
            else
            {
                Debug.LogError("Error on touch power update " + value);
            }
        }
    }

    public double CriticalRate { get; set; }

    public double CriticalValue { get; set; }
#pragma warning disable 0649
    [SerializeField]//temp
    private GemPool mGemPool;
#pragma warning restore 0649
    [SerializeField]
    private Gem mCurrentGem;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadGame();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        CalcStage(mUser.LastGemID);
        mCurrentGem.SetProgress((float)(mUser.Progress / mMaxProgress));
        UIController.Instance.ShowGaugeBar(mUser.Progress, mMaxProgress);
    }    

    private void OnApplicationQuit()
    {
        Save();
    }

    public int[] GetPlayerItemLevelArr()
    {
        return mUser.PlayerItemLevelArr;
    }

    public float[] GetSkillCooltimeArr()
    {
        return mUser.SkillCooltimeArr;
    }

    private void CalcStage(int id = -1)
    {
        mMaxProgress = 10 * Math.Pow(mProgressWeight, mUser.Stage);
        if(mCurrentGem != null)
        {
            mCurrentGem.gameObject.SetActive(false);
        }
        if(mUser.LastGemID < 0)
        {
            mUser.LastGemID = UnityEngine.Random.Range(0, Constants.TOTAL_GEM_COUNT);
        }
        mCurrentGem = mGemPool.GetFromPool(mUser.LastGemID);
        mIncome = 5 * Math.Pow(mIncomeWeight, mUser.Stage);
    }

    public void PowerTouch(double value)
    {
        if(value <= 0)
        {
            Debug.LogError("wrong power touch value " + value);
        }
        mUser.Progress += value;

        if(mUser.Progress >= mMaxProgress)
        {
            mUser.Gold += mIncome * (1 + IncomeBonus);
            mUser.Stage++;
            mUser.Progress = 0;
            CalcStage();
        }

        float progress = (float)(mUser.Progress / mMaxProgress);
        mCurrentGem.SetProgress(progress);

        UIController.Instance.ShowGaugeBar(mUser.Progress, mMaxProgress);
    }

    public void Touch()
    {
        if (mUser.Progress >= mMaxProgress)
        {
            mUser.Gold += mIncome * (1 + IncomeBonus);
            mUser.Stage++;
            mUser.Progress = 0;
            CalcStage();
        }
        else
        {
            double touchPower = mTouchPower;
            if (CriticalRate > UnityEngine.Random.Range(0, 1f))
            {
                touchPower = touchPower * (1 + CriticalValue);
            }

            mUser.Progress += mTouchPower;
            if(mUser.Progress > mMaxProgress)
            {
                mUser.Progress = mMaxProgress;
            }
            float progress = (float)(mUser.Progress / mMaxProgress);
            mCurrentGem.SetProgress(progress);
        }
        UIController.Instance.ShowGaugeBar(mUser.Progress, mMaxProgress);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
