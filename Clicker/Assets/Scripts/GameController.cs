using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    private double mGold;
    public Delegates.VoidCallback GoldCallback;
    public double Gold
    {
        get { return mGold; }
        set
        {
            if(value >= 0)
            {
                mGold = value;
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

    private int mCurrentStage;

    [SerializeField]
    private double mProgressWeight = 1.08d;
    private double mCurrentProgress;
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

    [SerializeField]//temp
    private GemPool mGemPool;

    [SerializeField]
    private Gem mCurrentGem;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            //TODO Load Save data
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        mCurrentStage = 0;
        mTouchPower = 1;
        CalcStage();
        UIController.Instance.ShowGaugeBar(mCurrentProgress, mMaxProgress);
    }

    private void CalcStage()
    {
        mMaxProgress = 10 * Math.Pow(mProgressWeight, mCurrentStage);
        if(mCurrentGem != null)
        {
            mCurrentGem.gameObject.SetActive(false);
        }
        mCurrentGem = mGemPool.GetFromPool(UnityEngine.Random.Range(0, Constants.TOTAL_GEM_COUNT));
        mIncome = 5 * Math.Pow(mIncomeWeight, mCurrentStage);
    }

    public void Touch()
    {
        if (mCurrentProgress >= mMaxProgress)
        {
            mGold += mIncome;
            mCurrentStage++;
            mCurrentProgress = 0;
            CalcStage();
        }
        else
        {
            mCurrentProgress += mTouchPower;
            if(mCurrentProgress > mMaxProgress)
            {
                mCurrentProgress = mMaxProgress;
            }
            float progress = (float)(mCurrentProgress / mMaxProgress);
            mCurrentGem.SetProgress(progress);
        }
        UIController.Instance.ShowGaugeBar(mCurrentProgress, mMaxProgress);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            mCurrentStage++;
            mMaxProgress = 10 * Math.Pow(mProgressWeight, mCurrentStage);
            UIController.Instance.ShowGaugeBar(mCurrentProgress, mMaxProgress);
        }
    }
}
