using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//外观，单例
public class GameFacade : MonoBehaviour
{
    //单例模式的定义
    public static GameFacade Instance;
    //资源工厂的对象调用
    private IssetFactory assetFactory;
    //登陆界面保存角色数据放在外观里面
    private int playerRole;
  
    
    
    
    
    
    
    
    public int PlayerRole
    {
        get => playerRole;
        set => playerRole = value;
    }

    private void Awake()
    {
        Instance = this;
        PlayerRole = PlayerPrefs.GetInt("Player");
        assetFactory = new ResourcesAsseetFacroty();
       

    }

    //
    // public void playersound(string name)
    // {
    //     if(GetComponent<AudioSource>().isPlaying)
    //         GetComponent<AudioSource>().Stop();
    //     GetComponent<AudioSource>().PlayOneShot(LoadSound(name));
    // }

}