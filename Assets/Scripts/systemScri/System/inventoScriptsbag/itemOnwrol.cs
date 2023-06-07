using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//游戏开始对背包初始化生成相对应的背包格子对其物品的存储
public class itemOnwrol : MonoBehaviour
{
   public Item ThisItem;

   public  Inventory playerInventory;

   private void OnTriggerEnter(Collider collider)
   {
      if (collider.transform.CompareTag("Player"))
      {
          AddNewItem();
          Destroy(gameObject);

      }
      
   }

   public void AddNewItem()
   {
       if (!playerInventory.ItemList.Contains(ThisItem))
       {
           // playerInventory.ItemList.Add(ThisItem);
           // inventoryManger.createNewItem(ThisItem);
           for (int i = 0; i < playerInventory.ItemList.Count; i++)
           {
               if (playerInventory.ItemList[i] == null)
               {
                   playerInventory.ItemList[i] = ThisItem;
                   break;
               }
           }
       }
       else
       {
           ThisItem.itemHeld += 1;
       }
       inventoryManger.Refreshtiem();
   }
}
