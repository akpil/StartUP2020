using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ImageDragging : MonoBehaviour, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Camera mCamera;
    [SerializeField]
    private UnityEvent PointerEnter;
    
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log(eventData.position);
        Vector3 pos = mCamera.ScreenToWorldPoint(eventData.position);
        Debug.Log(pos);
        pos.z = 0;

        transform.position = pos;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse Enter! " + eventData.position);
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse Exit! " + eventData.position);
    }
}
