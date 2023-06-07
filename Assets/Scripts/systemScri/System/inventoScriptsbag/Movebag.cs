using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//对背包的移动可以在面板随意移动背包
public class Movebag : MonoBehaviour,IDragHandler
{
    public Canvas Canvas;
    private RectTransform currentRect;

    
    public void OnDrag(PointerEventData eventData)
    {
        currentRect.anchoredPosition += eventData.delta;
    }


    void Awake()
    {
        currentRect = GetComponent<RectTransform>();
    }
}
