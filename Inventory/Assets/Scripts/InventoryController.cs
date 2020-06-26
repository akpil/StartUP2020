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

    [SerializeField]
    private EquipmentSlot[] mEquipSlotArr;
#pragma warning restore 0649
    private int mDraggingID, mEquipSlotID;
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

    // Start is called before the first frame update
    void Start()
    {
        mDraggingID = -1;
        mEquipSlotID = -1;
        for (int i = 0; i < 31; i++)
        {
            mItemInfoList.Add(mItemController.GetItem(i));
            mSlotArr[i].SetSprite(mItemController.GetItemSprite(i));
        }
        for (int i = 0; i < mEquipSlotArr.Length; i++)
        {
            mEquipSlotArr[i].Init(i);
        }
    }

    public void AddItem(ItemData data)
    {
        mItemInfoList.Add(data);
    }

    public void SetEquipSlotID(int id)
    {
        mEquipSlotID = id;
    }

    public bool StartDragging(int id)
    {
        Debug.Log("Start Dragging " + id);
        mDraggingID = id;
        //return mItemInfoList[id] != null && mItemInfoList[id].ID >= 0;
        return id < mItemInfoList.Count;
    }

    public void EndDragging()
    {
        if (mDraggingID >= 0 && mEquipSlotID >= 0)
        {
            ItemData item = mItemInfoList[mDraggingID];
            EquipmentSlot equipSlot = mEquipSlotArr[mEquipSlotID];
            if (item.ItemType == equipSlot.GetEquipType())
            {
                equipSlot.SetSprite(mItemController.GetItemSprite(item.ID));
            }
        }
    }


}
