using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private const int SLOT_COUNT = 35;
    public static InventoryController Instance;
    private List<ItemData> mItemInfoList;

#pragma warning disable 0649
    [SerializeField]
    private ItemController mItemController;
    [SerializeField]
    private InventorySlot mSlotPrefab;
    [SerializeField]
    private Transform mSlotParents;

    [SerializeField]
    private UnityEngine.UI.Image mDragTarget;
#pragma warning restore 0649

    private InventorySlot[] mSlotArr;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;

            mItemInfoList = new List<ItemData>();
            mSlotArr = new InventorySlot[SLOT_COUNT];
            for(int i = 0; i < SLOT_COUNT; i++)
            {
                mSlotArr[i] = Instantiate(mSlotPrefab, mSlotParents);
                mSlotArr[i].Init(i, null, mDragTarget);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddItem(ItemData data)
    {
        mItemInfoList.Add(data);
    }

    public bool StartDragging(int id)
    {
        Debug.Log("Start Dragging " + id);
        //return mItemInfoList[id] != null && mItemInfoList[id].ID >= 0;
        return id < mItemInfoList.Count;
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i =0; i < 31; i++)
        {
            mItemInfoList.Add(mItemController.GetItem(i));
            mSlotArr[i].SetSprite(mItemController.GetItemSprite(i));
        }
    }
}
