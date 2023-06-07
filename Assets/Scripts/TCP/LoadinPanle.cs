using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using  Command;

public class LoadinPanle : MonoBehaviour
{
    private Loaginrequest _loaginrequest;
    private InputField username;
    
    private InputField Password;
    public bool isLoadPan;
    public bool isLoadPan1;
    public Text hinttext;
    public Text tip;
    
    private void Start()
    {
        _loaginrequest = GetComponent<Loaginrequest>();
        username = transform.Find("username/InputField").GetComponent<InputField>();
        Password = transform.Find("password/InputField").GetComponent<InputField>();
        transform.Find("Load").GetComponent<Button>().onClick.AddListener(OnLoad);
        transform.Find("RegisterBtn").GetComponent<Button>().onClick.AddListener(OnRegis);
        
      //  hinttext=  GameObject.Find("hint").GetComponent<Text>();
    }

    private void Update()
    {
        if (isLoadPan == true)
        {
           
        

             gameObject.SetActive(false);
            //保存登入用户名
            ClientManger.instance.Username = username.text;
            
        }

        if (isLoadPan1)
        {
            tip.text = hinttext.text;
            isLoadPan1 = false;
        }
        
    }

    private void OnLoad()
    {
        if (string.IsNullOrEmpty(username.text))
        {
            hinttext.text="用户名不能为空";
            return;
        }

        if (string.IsNullOrEmpty(Password.text))
        {
            hinttext.text="密码不能为空";
            return;
        }

        //发送数据给服务器
        _loaginrequest.SendRequest(username.text,Password.text);
    }
    private void OnRegis()
    {
        tip.text = "";
        transform.parent.Find("RegisterPanel ").gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnlogonRequest(Returncode requestcode)
    {
        if (requestcode.Equals(Returncode.sucess))
        {
            isLoadPan = true;
            hinttext.text = "登陆成功";
            return;
        }
        else
        {
            isLoadPan1 = true;
            hinttext.text = "用户不存在";
            return;
        }
    }
}