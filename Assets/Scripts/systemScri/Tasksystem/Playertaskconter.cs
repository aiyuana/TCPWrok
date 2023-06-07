using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 这是一个人物接受任务的脚本，使用单例可以给npc调用
/// </summary>
public class Playertaskconter : MonoBehaviour
{
   public static Playertaskconter instance;
   public int exp;
   public int gold;
   public int itemAmont;
   
   public List<Quest> questList = new List<Quest>();//这里最好改为字典好点，public Dictionary<string,Quest> questDict=new Dictionary<string,Quest>();
  private void Awake()
   {
      if (instance == null)
      {
         instance = this;
      }
      else
      {
         if (instance != this)
         {
            Destroy(gameObject);
         }
      }
      // DontDestroyOnLoad(gameObject);
   }
}
