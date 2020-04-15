using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidPool : MonoBehaviour
{
    [SerializeField]
    private AsteroidMovement[] mOriginArr;
    private List<AsteroidMovement>[] mPool;
    // Start is called before the first frame update
    void Awake()
    {
        mPool = new List<AsteroidMovement>[mOriginArr.Length];
        for (int i = 0; i < mPool.Length; i++)
        {
            mPool[i] = new List<AsteroidMovement>();
        }
    }

    public AsteroidMovement GetFromPool(int id = 0)
    {
        for (int i = 0; i < mPool[id].Count; i++)
        {
            if(!mPool[id][i].gameObject.activeInHierarchy)
            {
                mPool[id][i].gameObject.SetActive(true);
                return mPool[id][i];
            }
        }

        AsteroidMovement newObj = Instantiate(mOriginArr[id]);
        mPool[id].Add(newObj);
        return newObj;
    }
}
