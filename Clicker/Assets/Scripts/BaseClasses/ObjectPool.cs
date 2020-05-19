using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T : Component
{
#pragma warning disable 0649
    [SerializeField]
    protected T[] mOriginArr;
#pragma warning restore 0649
    [SerializeField]
    protected List<T>[] mPools;
    // Start is called before the first frame update
    void Awake()
    {
        PoolSetup();
    }

    protected void PoolSetup()
    {
        mPools = new List<T>[mOriginArr.Length];
        for (int i = 0; i < mPools.Length; i++)
        {
            mPools[i] = new List<T>();
        }
    }

    public T GetFromPool(int id = 0)
    {
        for(int i = 0; i < mPools[id].Count; i++)
        {
            if(!mPools[id][i].gameObject.activeInHierarchy)
            {
                mPools[id][i].gameObject.SetActive(true);
                return mPools[id][i];
            }
        }
        return CreateNewObj(id);
    }

    virtual protected T CreateNewObj(int id)
    {
        T newObj = Instantiate(mOriginArr[id]);
        mPools[id].Add(newObj);
        return newObj;
    }
}
