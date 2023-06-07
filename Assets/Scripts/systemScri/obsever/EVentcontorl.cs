using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//观察者模式监听事件监听游戏结束还是继续
public enum Eventnum
{//定义枚举数据类型存储观察者和被观察者的事件码
    NONE,
    GAMOVer
}
//定义一个委托作为传递回调的函数
public delegate void CallBack();
//观察者定义D
public class EVentcontorl : MonoBehaviour
{
    //定义一个字典存放事件和委托
    private static Dictionary<Eventnum, Delegate> mevent = new Dictionary<Eventnum, Delegate>();
    //input,disable
    /// <summary>
    /// 判断监听的委托是否同一个类型
    /// </summary>
    /// <param name="eventype"></param>
    /// <param name="callBack"></param>
    private static void onListrnerAdding(Eventnum eventype, Delegate callBack)
   
    {
        if (!mevent.ContainsKey(eventype))
        {
            mevent.Add(eventype,null);
         
        }
    }
    /// <summary>
    /// 判断监听是否为空
    /// </summary>
    /// <param name="eventype"></param>
    private static void onListrnerRemoved(Eventnum eventype)
    {
        if (mevent[eventype]==null)
        {
            mevent.Remove(eventype);
         
        }
    }
//观察者添加监听
    public static void AddListener(Eventnum eventype, CallBack callBack)
    {
        onListrnerAdding(eventype,callBack);
        mevent[eventype] = (CallBack) mevent[eventype] +callBack;
    }//观察者移除监听

    public static void RemoveListener(Eventnum eventype, CallBack callBack)
    {
   
        mevent[eventype] = (CallBack) mevent[eventype] -callBack;
        onListrnerRemoved(eventype);
    }
    /// <summary>
    /// 被观察者发布消息
    /// </summary>
    /// <param name="eventype"></param>
    public static void BroadCast(Eventnum eventype)
    {
        Delegate d;
        if (mevent.TryGetValue(eventype, out d))
        {
            CallBack callBack = (CallBack) d;
            if(callBack!=null)
            {
                callBack();
            }
            else
            {
                Debug.LogError("事件不存在");
                throw new Exception("事件不存在");
            }
        }
   
    }


}