using System.Collections;
using System.Collections.Generic;
using Unity.Profiling.LowLevel.Unsafe;
using UnityEngine;
[System.Serializable]
public class Quest
{
   public enum QuestType
   {
      Gathering,
      Talk,
      Reach
   };

   public enum QuestStatus
   {
      waitting,
      Accepted,
      completed
   };

   public string questName;
   public QuestType questype;
   public QuestStatus questStatus;
   public int exRewards;
   public int goldRewards;
   //收集需要到达的数量
   public int requireAmount;
}
