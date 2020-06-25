using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDragHandler
{
    [SerializeField]
    private Image mItemImage;
    private int mID;

    public void Init(int id, Sprite image)
    {
        mID = id;
        mItemImage.sprite = image;
    }

    public void SetSprite(Sprite image)
    {
        mItemImage.sprite = image;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }
}
