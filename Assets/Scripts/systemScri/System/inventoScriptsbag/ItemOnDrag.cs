using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.EventSystems;
//背包的拖拽可以任意的位置进行位置交换
public class ItemOnDrag : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{

    public Transform originalParent;
    public Inventory mybag;
    private int currentItemId;
  

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        currentItemId = originalParent.GetComponent<Slot>().slotId;
        
        
        transform.SetParent(transform.parent.parent);
        transform.position = eventData.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position= eventData.position;
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.name == "ItemImage")
        {
            transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent);
            transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;
            //it,lis物品储存位置改变
            var temp = mybag.ItemList[currentItemId];
            mybag.ItemList[currentItemId] =
                mybag.ItemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotId];
            mybag.ItemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotId] = temp;
            
            
            eventData.pointerCurrentRaycast.gameObject.transform.parent.position = originalParent.position;
            eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(originalParent);
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            return;
        }
        //在slot下面
        transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
        transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
        mybag.ItemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotId] =
            mybag.ItemList[currentItemId];
        mybag.ItemList[currentItemId] = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
