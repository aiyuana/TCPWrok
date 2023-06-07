using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Command;
public class Loaginrequest :BaseRequest
{
    private LoadinPanle _loadinPanle;
    private void Start()
    {
        requestcode = Requestcode.User;
        actionCode = ActionCode.Login;
        ClientManger.instance.AddResquest(actionCode,this);
        _loadinPanle = GetComponent<LoadinPanle>();

        //   ClientManger.instance.AddResquest(actionCode,this);
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
        _loadinPanle.OnlogonRequest((Returncode)int.Parse(data));
    }
}