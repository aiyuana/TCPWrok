using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Player : MonoBehaviour
{
    private float timer = 0;
    public GameObject[] GameObjects;
    public SaveDataObject SObject;//先声明我们要存的对象
    public SaveDataObject[] game;
    public TextMeshProUGUI lever;
    //要用button的事件链接这两个函数
    private void Start()
    {
        Player player = GetComponent<Player>();
        SObject = new SaveDataObject(player);
        SaveSysteam.Load<SaveDataObject>(SObject);//和保存的使用方法一样
        gameObject.transform.position = new Vector3(SObject.X,SObject.Y,SObject.Z);
        
        
    }

    public void Update()
    {
        timer = timer + Time.deltaTime;
        if (timer<0.5f)
        {
            lever.text = SObject.lever1;
            //下面就是把获得的数据传回到游戏中了
         
        }
        
    }

    public void ClickSave()
    {
        Player player=GetComponent<Player>();//获得玩家对象
        SObject=new SaveDataObject(player,lever.text);//denghji
        SaveSysteam.Save<SaveDataObject>(SObject);//保存实际就这一行，传入我们要存的对象即可
    }
    
    public void ClickLoad()
    {
        Player player = GetComponent<Player>();
        SObject = new SaveDataObject(player);
        SaveSysteam.Load<SaveDataObject>(SObject);//和保存的使用方法一样
        //下面就是把获得的数据传回到游戏中了
        transform.position = new Vector3(SObject.X,SObject.Y,SObject.Z);
        Debug.Log(transform.position);
        Debug.Log(GameObject.FindWithTag("Player").transform.position);
        lever.text = SObject.lever1;
        Debug.Log("IsLoad");
    }
 
}
//这是我要存储的类型获得玩家名称和位置信息
//想存别的东西就现创建一个类就行了，不用动存档系统的代码哦，方便吧
[CanSave(author = "沅", versionNumber = "版本1.1",isCheck =true)]
public class SaveDataObject
{
    
    public float X;
    public float Y;
    public float Z;
    public string lever1;
 
    public  SaveDataObject(Player player,string lever)
    {
        X = player.transform.position.x;
        Y = player.transform.position.y;
        Z = player.transform.position.z;
        lever1 = lever;
    }
    public SaveDataObject(Player player)
    {
        X = player.transform.position.x;
        Y = player.transform.position.y;
        Z = player.transform.position.z;
        
    }
}