using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltPool : MonoBehaviour
{
    [SerializeField]
    private Bolt[] mPrefab;
    private List<Bolt>[] mPool;
    // Start is called before the first frame update
    void Start()
    {
        mPool = new List<Bolt>[mPrefab.Length];
        for(int i = 0; i < mPool.Length; i++)
        {
            mPool[i] = new List<Bolt>();
        }
    }

    public Bolt GetFromPool(int id = 0)
    {
        for(int i = 0; i < mPool[id].Count; i++)
        {
            if(!mPool[id][i].gameObject.activeInHierarchy)
            {
                mPool[id][i].gameObject.SetActive(true);
                return mPool[id][i];
            }
        }
        Bolt newObj = Instantiate(mPrefab[id]);
        mPool[id].Add(newObj);
        return newObj;
    }
}
