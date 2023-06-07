using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Questable : MonoBehaviour
{
   public Quest quest;
//可以委派任务的npc
   public void DelegateQuest()
   {
      if (quest.questStatus == Quest.QuestStatus.waitting)
      {//委托委派一个任务
         Playertaskconter.instance.questList.Add(quest);
         //防止二次添加任务到任务集合之中
         quest.questStatus = Quest.QuestStatus.Accepted;
      }
      else
      {
         //我们的任务已经接到了任务
         Debug.Log("已经领取了该任务不能在领取了");
      }
   }
}
