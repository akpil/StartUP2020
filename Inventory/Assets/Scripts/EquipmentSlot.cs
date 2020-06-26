using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipmentSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private eItemType mEquipType;
    [SerializeField]
    private Image mItemImage;

    private int mID;

    public void Init(int id)
    {
        mID = id;
    }

    public void SetSprite(Sprite image)
    {
        mItemImage.sprite = image;
    }

    public eItemType GetEquipType()
    {
        return mEquipType;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        InventoryController.Instance.SetEquipSlotID(mID);
        Debug.Log(mID);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryController.Instance.SetEquipSlotID(-1);
        Debug.Log(-1);
    }
}
