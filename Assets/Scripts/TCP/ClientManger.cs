using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using  System.Net;
using System.Text;
using Command;


public class ClientManger : MonoBehaviour
{
    public static ClientManger instance;
    
    public string Ip;
    public int PoRT; 
    private Socket clientSocket;
    private message ms = new message();

    private Dictionary<ActionCode,BaseRequest> requestDict = new Dictionary<ActionCode, BaseRequest>();

    private string username;

    public string Username
    {
        get => username;
        set => username = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        clientSocket.Connect(IPAddress.Parse(Ip),PoRT);
        clientSocket.BeginReceive(ms.Data,ms.StartIndex,ms.Reaninszie,SocketFlags.None,ReceiveCallBack,null);
    }

    private void Awake()
    {
        instance = this;
    }

    private void OnDestroy()
    {
        clientSocket.Close();
    }
//向服务器发送公共方法
    public void SendRequset(Requestcode requestcode, ActionCode actionCode, string data)
    {//把要发送的转成字节数组
        //实际数据长度dataLengh(为了优化粘包问题）+requsetcode+Actioncode（操作事件）+data
        byte[] byteData = message.PackData(requestcode,actionCode,data);
        clientSocket.Send(byteData);
        Debug.Log("数据发送数据"+data);

    }
    private void ReceiveCallBack(IAsyncResult ar)
    {
        int count = clientSocket.EndReceive(ar);//接收到服务器一次的长度
        ms.ReadMessage(count,OnprocessMessage);
        //递归
        clientSocket.BeginReceive(ms.Data,ms.StartIndex,ms.Reaninszie,SocketFlags.None,ReceiveCallBack,null);

    }
    private void OnprocessMessage(ActionCode actioncode,string data)
    {
        Debug.Log("接收到服务器" + data);
        BaseRequest baseRequest;
        requestDict.TryGetValue(actioncode, out  baseRequest);
        baseRequest.OnResponse(data);
        
    }

    public void AddResquest(ActionCode actionCode,BaseRequest baseRequest)
    {
        requestDict.Add(actionCode,baseRequest);
    }
    public void RemoveResquest(ActionCode actionCode)
    {
        requestDict.Remove(actionCode);
    }
}
