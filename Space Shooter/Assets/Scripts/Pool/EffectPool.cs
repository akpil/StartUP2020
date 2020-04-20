using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eEffectType
{
    ExpAst,
    ExpEnemy,
    ExpPlayer
}

public class EffectPool : MonoBehaviour
{
    [SerializeField]
    private Timer[] mPrefab;
    private List<Timer>[] mPool;
    // Start is called before the first frame update
    void Awake()
    {
        mPool = new List<Timer>[mPrefab.Length];
        for(int i = 0; i < mPool.Length; i++)
        {
            mPool[i] = new List<Timer>();
        }
    }

    public Timer GetFromPool(int id = 0)
    {
        for (int i = 0; i < mPool[id].Count; i++)
        {
            if (!mPool[id][i].gameObject.activeInHierarchy)
            {
                mPool[id][i].gameObject.SetActive(true);
                return mPool[id][i];
            }
        }
        Timer newObj = Instantiate(mPrefab[id]);
        mPool[id].Add(newObj);
        return newObj;
    }
}
