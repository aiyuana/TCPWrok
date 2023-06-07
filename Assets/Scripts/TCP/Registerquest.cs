using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Command;
public class Registerquest : BaseRequest
{
    private Registerpanle registerpanle;
    private void Start()
    {
        requestcode = Requestcode.User;
        actionCode = ActionCode.Register;
        ClientManger.instance.AddResquest(actionCode,this);
        registerpanle = GetComponent<Registerpanle>();

      
    }
    private void OnDestroy()
    {
        ClientManger.instance.RemoveResquest(actionCode);
    }
    //发送请求
    public void SendRequest(string username, string passward)
    {
        string data = username + "," + passward;
        //通过clo发送请求
        ClientManger.instance.SendRequset(requestcode,actionCode,data);
    }
    
    public  override void OnResponse(string data)
    {
        base.OnResponse(data);
        registerpanle.OnregiRequest((Returncode)int.Parse(data));
    }
}