using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Command;
//所有request继承的父类
//继承的父类处理服务器响应作为数据类型储存在字典分发事件
public class BaseRequest : MonoBehaviour
{
    protected Requestcode requestcode;//向服务器发送请求代码
    protected ActionCode actionCode;//处理服务器响应事件代码
    public  virtual  void SendRequst(){}//发送请求给服务器
    public  virtual  void OnResponse(string data){}//处理服务器的响应
    
}