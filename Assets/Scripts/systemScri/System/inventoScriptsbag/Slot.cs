using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//格子的文字图片设置
public class Slot : MonoBehaviour
{
    //记住编号顺序
    public int slotId;//空格
    //获取到主键图片
    public Item slotItem;
    public Image SlotImage;
    public Text sloNum;
    public string slotInfo;

    public GameObject iteminsolt;

    public void ItemOnClicked()
    {
        inventoryManger.UpdateItemInfo(slotInfo);
    }

    public void setslot(Item item)
    {
        if (item == null)
        {
            iteminsolt.SetActive(false);
            return;
        }

        SlotImage.sprite = item.itemImage;
        sloNum.text = item.itemHeld.ToString();
        slotInfo = item.iteminfo;

    }
}
