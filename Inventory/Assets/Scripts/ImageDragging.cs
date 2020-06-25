using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ImageDragging : MonoBehaviour, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
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
