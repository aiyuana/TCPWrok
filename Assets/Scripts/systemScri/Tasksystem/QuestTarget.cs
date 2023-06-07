using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//去找到开关
//收集沿途道具并返回
//该脚本要放在任务npc或者物品
public class QuestTarget : MonoBehaviour
{
   public string questname;

   public enum QuestType
   {
      Gathering,
      Talk,
      Reach
   };

   public QuestType questType;
   public int amount = 1;
   public bool hasTalked;
   public bool hasReached;
   //任务完成后被调用，比如收集所有物品或者找到特殊区域，与特殊npc对话
   public void QuestComplete()
   {
      for (int i = 0; i < Playertaskconter.instance.questList.Count; i++)
      {
         if (questname == Playertaskconter.instance.questList[i].questName &&
             Playertaskconter.instance.questList[i].questStatus == Quest.QuestStatus.Accepted)
         {
            switch (questType)
            {
               case QuestType.Gathering :
                  if (Playertaskconter.instance.itemAmont >= Playertaskconter.instance.questList[i].requireAmount)
                  {
                     Playertaskconter.instance.questList[i].questStatus = Quest.QuestStatus.completed;
                     QuestManger.instance.UpdateQuestList();
                  }
                  break;
               case QuestType.Reach:
                  if (hasReached)
                  {
                     Playertaskconter.instance.questList[i].questStatus = Quest.QuestStatus.completed;
                     QuestManger.instance.UpdateQuestList();
                  }
                  break;
               case  QuestType.Talk:
                  if (hasTalked)
                  {
                     Playertaskconter.instance.questList[i].questStatus = Quest.QuestStatus.completed;
                     QuestManger.instance.UpdateQuestList();
                  }
                  break;
            }
         }
      }
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         if (questType == QuestType.Gathering)
         {
            Playertaskconter.instance.itemAmont += amount;
            QuestComplete();
         }
         else if (questType == QuestType.Reach)
         {
            hasReached = true;
            QuestComplete();
         }
      }
   }
}
