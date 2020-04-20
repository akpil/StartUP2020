using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField]
    private Enemy mPrefab;
    private List<Enemy> mPool;
    [SerializeField]
    private BoltPool mEnemyBoltPool;
    // Start is called before the first frame update
    void Awake()
    {
        mPool = new List<Enemy>();
    }

    public Enemy GetFromPool()
    {
        for (int i = 0; i < mPool.Count; i++)
        {
            if (!mPool[i].gameObject.activeInHierarchy)
            {
                mPool[i].gameObject.SetActive(true);
                return mPool[i];
            }
        }
        Enemy newObj = Instantiate(mPrefab);
        newObj.BoltPool = mEnemyBoltPool;
        mPool.Add(newObj);
        return newObj;
    }
}
