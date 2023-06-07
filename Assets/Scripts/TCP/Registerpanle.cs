using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using  Command;

public class Registerpanle : MonoBehaviour
{
    private Registerquest registerquest;
    private InputField username;
    private InputField Password;
    private bool isLoadPan;
    public Text hinttext;
    private void Start()
    {
        registerquest = GetComponent<Registerquest>();
        username = transform.Find("username/InputField").GetComponent<InputField>();
        Password = transform.Find("password/InputField").GetComponent<InputField>();
        transform.Find("registerbtn").GetComponent<Button>().onClick.AddListener(Onregi);
        hinttext=  GameObject.Find("hint").GetComponent<Text>();
    }

    private void Update()
    {
        if (isLoadPan == true)
        {
            transform.parent.Find("LoadPanel").gameObject.SetActive(true);
        

            gameObject.SetActive(false);
        }

        isLoadPan =false;
    }

    private void Onregi()
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
        registerquest.SendRequest(username.text,Password.text);
    }
  

    public void OnregiRequest(Returncode requestcode)
    {
        if (requestcode.Equals(Returncode.sucess))
        {
            isLoadPan = true;
            hinttext.text="注册成功";
            username.text = "";
            Password.text = "";
        }
        else
        {
            hinttext.text="注册失败";
        }
    }
}