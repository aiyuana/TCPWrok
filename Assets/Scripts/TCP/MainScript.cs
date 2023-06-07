using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  Command;
public class Mainrequest : BaseRequest
{
    private MainScript _mainScript;
    private void Start()
    {
        requestcode = Requestcode.Chat;
        actionCode = ActionCode.Handlechat;
        ClientManger.instance.AddResquest(actionCode,this);
        _mainScript = GetComponent<MainScript>();

      
    }
    private void OnDestroy()
    {
        ClientManger.instance.RemoveResquest(actionCode);
    }
    //发送请求
    public void SendRequest(string chat)
    {
      
        //通过clo发送请求
        ClientManger.instance.SendRequset(requestcode,actionCode,chat);
    }
    
    public  override void OnResponse(string data)
    {
        base.OnResponse(data);
        _mainScript.OnResponse(data);

    }
}