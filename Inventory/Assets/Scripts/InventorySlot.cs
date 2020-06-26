using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private Camera mUICamera;
    [SerializeField]
    private Image mItemImage;
    private int mID;
    private Image mDragTarget;
    private bool mbDragging;
    

    public void Init(int id, Sprite image, Image dragTarget)
    {
        mID = id;
        mItemImage.sprite = image;
        mDragTarget = dragTarget;
    }

    public void SetSprite(Sprite image)
    {
        mItemImage.sprite = image;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        mbDragging = InventoryController.Instance.StartDragging(mID);
        if (mbDragging) // 몇번째 슬롯이 드래그 시작인지
        {
            mDragTarget.sprite = mItemImage.sprite;
            mItemImage.color = Color.clear;
            mDragTarget.transform.position = eventData.position;
            mDragTarget.gameObject.SetActive(true);
        }
        // Screen space Camera 옵션일 경우 사용
        //Vector3 pos =  mUICamera.ScreenToWorldPoint(eventData.position);
        //pos.z = 0;

        //drag target 세팅
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Screen space Camera 옵션일 경우 사용
        //Vector3 pos = mUICamera.ScreenToWorldPoint(eventData.position);
        //pos.z = 0;

        if(mbDragging)
        {
            mDragTarget.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(mbDragging)
        {
            Debug.Log("End Drag");
            mItemImage.color = Color.white;
            mDragTarget.gameObject.SetActive(false);
            mbDragging = false;
            InventoryController.Instance.EndDragging();
        }
    }
}
