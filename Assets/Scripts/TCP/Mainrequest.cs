using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{
    public GameObject TextPrefab; //生成预设
    public Transform patentTra; //生成父物体
    public InputField MessageIF; //输入框内容
    public ScrollRect scrollRect; //滑动条移动到最下面
    public Mainrequest _mainrequest;


    private bool issendInfo = false;//是否同步聊天内容转到主线程
    private string data;//临时变量转到主线程处理

    private void UpdateMessage(string username, string info)
    {
        Instantiate(TextPrefab, patentTra).GetComponent<Text>().text =
          username + ":" + info;
        Debug.Log("" + ClientManger.instance.Username + ":" + MessageIF.text);
        //开启协程
        StartCoroutine(nameof(scrollToBotoo));
      
    }
    private void Update()
    {
        if (issendInfo)
        {
            issendInfo = false;
            string[] str = data.Split('|');
            //判断服务器发来不是当前用户，避免重复发送
            
            if(ClientManger.instance.Username.Equals(str[0]))
            {
                
                Debug.Log("1111");
                return;
            }
            UpdateMessage(str[0],str[1]);
            
            
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (string.IsNullOrEmpty(MessageIF.text)) return;

            //发到服务器
            Instantiate(TextPrefab, patentTra).GetComponent<Text>().text =
                ClientManger.instance.Username + ":" + MessageIF.text;
            Debug.Log("" + ClientManger.instance.Username + "|" + MessageIF.text);
            //开启协程
            StartCoroutine(nameof(scrollToBotoo));
            _mainrequest.SendRequest(ClientManger.instance.Username + "|" + MessageIF.text);
        }
    }

    IEnumerator scrollToBotoo()
    {
        yield return null;
        scrollRect.verticalNormalizedPosition = 0;
    }

    public void OnResponse(string data)
    {
        issendInfo = true;
        this.data = data;
    }
}
//处理请求响应

