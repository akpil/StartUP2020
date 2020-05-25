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

    public int LanguageType { get; set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadGame();
            if(Application.systemLanguage == SystemLanguage.Korean)
            {
                Debug.Log("Kor" + (int)Application.systemLanguage);
                LanguageType = 0;
            }
            else
            {
                Debug.Log("Non Kor" + (int)Application.systemLanguage);
                LanguageType = 1;
            }
            LanguageType = 1;
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

    public float[] GetSkillMaxCooltimeArr()
    {
        return mUser.SkillMaxCooltimeArr;
    }

    public int[] GetCoworkerLevelArr()
    {
        return mUser.CoworkerLevelArr;
    }

    public void Rebirth()
    {
        if(mUser.Stage >= 100)
        {
            #region 소울 지급
            double reward = mUser.Stage * 2;
            int levelTotal = 0;
            for (int i = 0; i < mUser.PlayerItemLevelArr.Length; i++)
            {
                levelTotal += mUser.PlayerItemLevelArr[i];
            }
            for (int i = 0; i < mUser.CoworkerLevelArr.Length; i++)
            {
                if (mUser.CoworkerLevelArr[i] > 0)
                {
                    levelTotal += mUser.CoworkerLevelArr[i];
                }
            }
            reward += levelTotal;
            mUser.Soul += reward;
            #endregion

            #region 레벨 & 스킬 쿨타임 초기화
            mUser.PlayerItemLevelArr = new int[Constants.PLAYER_ITEM_COUNT];
            mUser.PlayerItemLevelArr[0] = 1;
            mUser.SkillCooltimeArr = new float[Constants.SKILL_COUNT];
            mUser.SkillMaxCooltimeArr = new float[Constants.SKILL_COUNT];

            mUser.CoworkerLevelArr = new int[Constants.COWORKER_COUNT];
            for (int i = 0; i < mUser.CoworkerLevelArr.Length; i++)
            {
                mUser.CoworkerLevelArr[i] = -1;
            }
            mUser.CoworkerLevelArr[0] = 0;
            #endregion

            PlayerUpgradeController.Instance.Rebirth(mUser.PlayerItemLevelArr,
                                                     mUser.SkillCooltimeArr,
                                                     mUser.SkillMaxCooltimeArr);
            CoworkerController.Instance.Rebirth(mUser.CoworkerLevelArr);
            mUser.Gold = 0;
            mUser.Stage = 0;
            mUser.Progress = 0;
            mCurrentGem.gameObject.SetActive(false);
            CalcStage();

            UIController.Instance.ShowGaugeBar(mUser.Progress, mMaxProgress);
            //show gold
            //show soul
        }
        else
        {
            Debug.Log("환생 조건이 충족되지 않았습니다."); // Popup
        }
    }

    private IEnumerator BossCountDown(float time)
    {
        WaitForFixedUpdate frame = new WaitForFixedUpdate();
        while(time > 0)
        {
            yield return frame;
            time -= Time.fixedDeltaTime;
            // 보스 게이지 갱신
        }
        mBossCountDown = null;
        //패널티 부여
    }
    private Coroutine mBossCountDown;
    private void CalcStage(int id = -1)
    {
        if (mUser.Stage > 0 && mUser.Stage % 10 == 0)
        {
            mMaxProgress = 100 * Math.Pow(mProgressWeight, mUser.Stage);
            float bossTime = 30;
            // 보스 게이지 갱신
            mBossCountDown = StartCoroutine(BossCountDown(bossTime));
        }
        else
        {
            mMaxProgress = 10 * Math.Pow(mProgressWeight, mUser.Stage);
            if(mBossCountDown != null)
            {
                StopCoroutine(mBossCountDown);
                mBossCountDown = null;
            }
        }
        
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
