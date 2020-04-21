using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField]
    private Vector3 mSpawnStartPos, mSpawnEndPos;
    [SerializeField]
    private float mSpawnTime;
    private EffectPool mEffectPool;

    [SerializeField]
    private int mMaxHP;
    private int mCurrentHP;
    private bool mbInvincible;
    [SerializeField]
    private Transform[] mPosArr;
    [SerializeField]
    private float mPosMoveTime;

    [SerializeField]
    private BoltPool mBoltPool;
    [SerializeField]
    private Transform mBoltPos;
    [SerializeField]
    private Player mPlayer;

    private Coroutine mPhaseRoutine;

    public bool IsAlive()
    {
        return mCurrentHP > 0;
    }

    private void Awake()
    {
        mEffectPool = GameObject.FindGameObjectWithTag("EffectPool").GetComponent<EffectPool>();
    }

    private void OnEnable()
    {
        transform.position = mSpawnStartPos;
        mbInvincible = true;
        mCurrentHP = mMaxHP;
        StartCoroutine(Appear());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private IEnumerator Appear()
    {
        Vector3 startSize = Vector3.one * 1.5f;
        Vector3 endSize = Vector3.one * 3;
        WaitForFixedUpdate frame = new WaitForFixedUpdate();
        float currentTime = 0;
        float progress = 0;
        while(currentTime <= mSpawnTime)
        {
            yield return frame;
            currentTime += Time.fixedDeltaTime;
            progress = currentTime / mSpawnTime;
            transform.position = Vector3.Lerp(mSpawnStartPos, mSpawnEndPos, progress);
            transform.localScale = Vector3.Lerp(startSize, endSize, progress);
        }
        mbInvincible = false;
        mPhaseRoutine = StartCoroutine(Phase());
    }

    private IEnumerator Phase()
    {
        WaitForFixedUpdate frame = new WaitForFixedUpdate();
        WaitForSeconds pointThree = new WaitForSeconds(.3f);
        float currentTime = 0;
        float progress = 0;
        int startIndex = 1;
        int endIndex = 0;
        bool bAscend = true;
        while(true)
        {
            Vector3 startPos = mPosArr[startIndex].position;
            Vector3 endPos = mPosArr[endIndex].position;
            progress = 0;
            currentTime = 0;
            while (currentTime <= mPosMoveTime)
            {
                yield return frame;
                currentTime += Time.fixedDeltaTime;
                progress = currentTime / mPosMoveTime;
                transform.position = Vector3.Lerp(startPos, endPos, progress);
            }

            for (int i = 0; i < 3; i++)
            {
                Bolt bolt = mBoltPool.GetFromPool(1);
                bolt.transform.position = mBoltPos.position;
                bolt.transform.LookAt(mPlayer.transform);
                bolt.ReSetDir();
                yield return pointThree;
            }

            startIndex = endIndex;
            if(bAscend)
            {
                endIndex++;
                bAscend = endIndex < mPosArr.Length - 1;
            }
            else
            {
                endIndex--;
                bAscend = endIndex == 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Bolt"))
        {
            if(!mbInvincible)
            {
                mCurrentHP--;
                Debug.LogFormat("{0}/{1}", mCurrentHP, mMaxHP);
                if(mCurrentHP <= 0)
                {
                    mbInvincible = true;
                    StopCoroutine(mPhaseRoutine);
                    StartCoroutine(Die());
                }
            }
            Timer effect = mEffectPool.GetFromPool((int)eEffectType.ExpAst);
            effect.transform.position = other.ClosestPoint(transform.position);
            other.gameObject.SetActive(false);
        }
    }

    private IEnumerator Die()
    {
        WaitForSeconds pointThree = new WaitForSeconds(.3f);

        for (int i = 0; i < 10; i++)
        {
            yield return pointThree;
            Timer effect = mEffectPool.GetFromPool((int)eEffectType.ExpEnemy);
            effect.transform.position = transform.position + Random.insideUnitSphere * 4 + Vector3.up;
        }
        gameObject.SetActive(false);
    }
}
