using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    private int mCurrentStage;

    [SerializeField]
    private double mProgressWeight = 1.08d;
    private double mCurrentProgress;
    private double mMaxProgress;

    private double mTouchPower;

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
        mMaxProgress = 10;
        mTouchPower = 1;
        mCurrentGem = mGemPool.GetFromPool(UnityEngine.Random.Range(0, Constants.TOTAL_GEM_COUNT));
    }

    private void CalcNextStage()
    {
        mCurrentStage++;
        mMaxProgress = 10 * Math.Pow(mProgressWeight, mCurrentStage);
        mCurrentGem.gameObject.SetActive(false);
        mCurrentGem = mGemPool.GetFromPool(UnityEngine.Random.Range(0, Constants.TOTAL_GEM_COUNT));
        mCurrentProgress = 0;
    }

    public void Touch()
    {
        if (mCurrentProgress >= mMaxProgress)
        {
            CalcNextStage();
        }
        else
        {
            mCurrentProgress += mTouchPower;
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
