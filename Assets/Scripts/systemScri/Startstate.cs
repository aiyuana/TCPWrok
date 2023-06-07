using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

//委托，代理模式//观察者
public class Startstate : ISceneState
{  public Startstate(SceneContorl sceneContorl) : base("Start", sceneContorl) { }

    private Transform mUIRoot;
    //开始页面
    private Transform  startbg;
    private Transform registerbg;
    private Transform singbg;
    private Text hinttext;
    private Text hinttext1;
    private LoadinPanle _loadinPanle;
    public override void StateStart()
    {
       
        
        Init();
        MyOnEnable();
        
    }

    public override void  StateUpdate()
    {
        if (_loadinPanle.isLoadPan==true)
        {
                   
            mController.Setstate(new LoadState(mController));
                    
        }
    }
    private void Init()
    {
        mUIRoot = GameObject.Find("Canvas").transform;
        registerbg=mUIRoot.Find("RegisterPanel ");
        singbg =mUIRoot.Find("LoadPanel"); 
        startbg=mUIRoot.Find("StartBG");
        regisUsername = registerbg.transform.Find("username/InputField").GetComponent<InputField>();
        UsernameInput = singbg.transform.Find("username/InputField").GetComponent<InputField>();
        repassword= registerbg.transform.Find("password/InputField").GetComponent<InputField>();
        Password= singbg.transform.Find("password/InputField").GetComponent<InputField>();
        hinttext=  GameObject.Find("hint").GetComponent<Text>();
        _loadinPanle = singbg.GetComponent<LoadinPanle>();
    }


    //注册界面
    

    private InputField regisUsername;
    private InputField repassword;
    private Dropdown resexy;
    private InputField reage;
    
   
    //登入界面
 
    private InputField UsernameInput;
    private InputField Password;
//提示文本
    private string Password1;
    private string  username;
    private string Age;
    private string sexy;

    
   
    
    void Start()
    {
        
        // sqlstats.Instance.text1 = (e, r) =>
        // {
        //     hinttext.text = e;
        //     r.Invoke(gameObject.name);
        // };//回调参数的委托，获取到事件
       
    }
    //注册界面方法
   
    private void emptytex()
    {
        regisUsername.text = "";
        repassword.text = "";
        UsernameInput.text = "";
        Password.text = "";
        hinttext.text = "";
       
    }
    private void MyOnEnable()
    {
        //开始界面功能实现
        List<Button> loainWindowButtonList = new List<Button>();
        loainWindowButtonList.AddRange(startbg.GetComponentsInChildren<Button>());//添加整个列表元素到列表里面
        foreach (Button button in loainWindowButtonList)
        {
            button.onClick.AddListener(() =>
            {
                LoginWindowButtonClick(button);

            });
        }
        //登入界面的代码实现
        List<Button> signButtonList = new List<Button>();//创建一个按钮的数列
        signButtonList.AddRange(singbg.GetComponentsInChildren<Button>());
        foreach (Button  button1 in signButtonList)
        {
            button1.onClick.AddListener(() =>
            {
                LoginWindowButtonClick(button1);
            });
        }

        List<Button> reButtonList = new List<Button>();
        reButtonList.AddRange(registerbg.GetComponentsInChildren<Button>());
        foreach (Button  button2 in reButtonList)
        {
            button2.onClick.AddListener(() =>
            {
                LoginWindowButtonClick(button2);
            });
        }

    }
    //模式管理场景的按钮
   
        private void LoginWindowButtonClick(Button sender)
          {
  
        switch (sender.name)
        {
            case "Startgame":

                {
                    Debug.Log("进入游戏");
                    startbg.gameObject.SetActive(false);
                    singbg.gameObject.SetActive(true);
                    emptytex();
                }
                break;

            case "exit":
                {

                    Application.Quit();


                }
                break;
            case "RegisterBtn":
                {
                    
                    singbg.gameObject.SetActive(false);
                    registerbg.gameObject.SetActive(true);
                    emptytex();

                }
                break;
            
            case "back":
            {
                
                singbg.gameObject.SetActive(true);
               registerbg.gameObject.SetActive(false);
               emptytex();
            }
                break;

            case "Load":
            {
                
                
            }
                break;
        
           
            default:
                break;
        }
    }


}
