using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


//主游戏场景控制器
public class GameState : ISceneState
{
    public GameState(SceneContorl sceneContorl) : base("Game", sceneContorl)
    {
    }

    private Transform mUIRoot;
    private Transform uesmange;
    private Transform masege;
    private Button setting;
    private Transform bags;
    private Button bages;
    public Transform usebok;
    private Button userbook;
  
    private Button usemange;
    private Text name;
    private Text sexy;
    private Text age;
    private Text leve;
    private Text uesr;
    private Transform panel;
    private Transform panel1;
    private Transform winpanel;
    private string passwrod;
    private InputField inputpasswrod;
    private Transform tack;
    private Button tackbut;
    public override void StateStart()
    {  
        Init();
        MyOnEnable();
        sqlstats.Instance.mangerss = (n, s, a, lv) =>
        {
            name.text = (n);
            uesr.text = (n);
            sexy.text = (s);
            Debug.Log(s);
            Debug.Log(a);
            age.text = (a).ToString();
            leve.text = (lv);
        };
        
        EVentcontorl.AddListener(Eventnum.GAMOVer,showEndpanel);
    }

    public override void StateEnd()
    {
        EVentcontorl.RemoveListener(Eventnum.GAMOVer,showEndpanel);
       
    }

    private void showEndpanel()
        {

            panel1.gameObject.SetActive(true);
          
        }

        

        // public override void StateEnd()
        // {
        //     EVentcontorl.RemoveListener(Eventnum.GAMOVer,GameOver);
        // }

        private void Init()
    {
        mUIRoot = GameObject.Find("playerUI").transform;
        uesmange = mUIRoot.Find("Playermange");
        masege = mUIRoot.Find("Manages");
       
        panel = mUIRoot.Find("Gameoverpanel");
        panel1 = mUIRoot.Find("overgame");
        winpanel = mUIRoot.Find("Win");
        uesr = mUIRoot.Find("name").GetComponent<Text>();
        name = masege.GetChild(0).GetComponent<Text>();
        sexy = masege.GetChild(1).GetComponent<Text>();
        age = masege.GetChild(2).GetComponent<Text>();
        leve = masege.GetChild(3).GetComponent<Text>();
        inputpasswrod = masege.transform.Find("Inputopasddwrod").GetComponent<InputField>();
        setting = mUIRoot.Find("setting").GetComponent<Button>();
        //bags = mUIRoot.Find("Bag");
      // tack = mUIRoot.Find("MainPanel");
      //  bages= mUIRoot.Find("bages").GetComponent<Button>();
        usebok = mUIRoot.Find("usebookPanel");
        userbook = mUIRoot.Find("userbook").GetComponent<Button>();
        usemange = uesmange.Find("usemange").GetComponent<Button>();
        //tackbut = mUIRoot.Find("Tack").GetComponent<Button>();
        
    }

    private void MyOnEnable()
    {
      
        List<Button> ButtonList = new List<Button>();
        ButtonList.AddRange(uesmange.GetComponentsInChildren<Button>()); //添加整个列表元素到列表里面
        foreach (Button button in ButtonList)
        {
            button.onClick.AddListener(() =>
            {
               ButtonClick(button);

            });
        }
        List<Button> ButtonList1 = new List<Button>();
        ButtonList1.AddRange(masege.GetComponentsInChildren<Button>()); //添加整个列表元素到列表里面
        foreach (Button button in ButtonList1)
        {
            button.onClick.AddListener(() =>
            {
                ButtonClick(button);

            });
        }

        List<Button> ButtonList2 = new List<Button>();
        ButtonList2.AddRange(panel.GetComponentsInChildren<Button>());
        foreach (Button button in  ButtonList2)
        {
            button.onClick.AddListener(() =>
            {
                ButtonClick(button);
            });
        }
        List<Button> ButtonList3 = new List<Button>();
        ButtonList3.AddRange(panel1.GetComponentsInChildren<Button>());
        foreach (Button button in  ButtonList3)
        {
            button.onClick.AddListener(() =>
            {
                ButtonClick(button);
            });
        }
        List<Button> ButtonList4 = new List<Button>();
        ButtonList4.AddRange(winpanel.GetComponentsInChildren<Button>());
        foreach (Button button in  ButtonList4)
        {
            button.onClick.AddListener(() =>
            {
                ButtonClick(button);
            });
        }
        setting.onClick.AddListener(settingss);
       // bages.onClick.AddListener(bagess);
       // usemange.onClick.AddListener(usemanges);
        userbook.onClick.AddListener(userbooks);
       // tackbut.onClick.AddListener(tackconter);
    }

    void settingss()
    {
        time.Invoke(true);
        panel.gameObject.SetActive(true);
        
    }

    void bagess()
    {
     bags.gameObject.SetActive(true);   
    }
    void usemanges()
    {
        masege.gameObject.SetActive(true);
    }
    void userbooks()
    {
        usebok.gameObject.SetActive(true);
    }

    void tackconter()
    {
        tack.gameObject.SetActive(true);
    }
    
    //模式管理场景的按钮
    public static Action<bool> time;
    
    private void ButtonClick(Button sender)
    {

        switch (sender.name)
        {
            case "usemange":
            {
                time.Invoke(true);
               //sqlstats.Instance.readdate();
            }

                break;
            case "back":
            {
                time.Invoke(false);
                Time.timeScale = 1;
            }
                break;

            case "Logoff":
            {
                time.Invoke(false);
               // sqlstats.Instance.delectuser(uesr.text);
                mController.Setstate(new Startstate(mController));
            }
                break;
            case "repasswrod":
            {
                passwrod = inputpasswrod.text.Trim();
                //sqlstats.Instance.selectpasswrod(new[] {uesr.text, passwrod});
            }
                break;
            case "RegameButton":
            {
              
                mController.Setstate(new GameState(mController));
            
            }
                break;
            
            case "overgameButton":
            {
                 // UnityEditor.EditorApplication.isPlaying = false;
                Application.Quit();
            }
                break;
            case "back1":
            {
              time.Invoke(false);
            }break;
            case "gameoverback":
            {
                // UnityEditor.EditorApplication.isPlaying = false;
                Application.Quit();
            }
                break;
            case "reback":
            {
                mController.Setstate(new GameState(mController));
            }
                break;
            case "Tack":
            {
               
            }
                break;
            
            
            default:
                break;
        }
    }
}

