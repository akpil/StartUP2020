using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviour
{
    [SerializeField]
    private Item[] mOriginArr;
    private List<Item>[] mPool;
    [SerializeField]
    private ItemController mController;
    // Start is called before the first frame update
    void Awake()
    {
        mPool = new List<Item>[mOriginArr.Length];
        for (int i = 0; i < mPool.Length; i++)
        {
            mPool[i] = new List<Item>();
        }
    }

    public Item GetFromPool(int id = 0)
    {
        for (int i = 0; i < mPool[id].Count; i++)
        {
            if (!mPool[id][i].gameObject.activeInHierarchy)
            {
                mPool[id][i].gameObject.SetActive(true);
                return mPool[id][i];
            }
        }

        Item newObj = Instantiate(mOriginArr[id]);
        mPool[id].Add(newObj);
        newObj.SetController(mController);
        return newObj;
    }
}
