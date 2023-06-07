using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

/// <summary>
/// 任务ui单例
/// </summary>
public class QuestManger : MonoBehaviour
{
    
    
    public static QuestManger instance;
//ui里面三个任务列表
    public GameObject[] questuiArry;
    //ui列表
    private GameObject questpanel;

    private void Start()
    {questpanel=GameObject.Find("playerUI").transform.Find("Qust List").gameObject;
        questpanel.SetActive(false);
    }
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }
    }
//创建一个接到任务之后会在UI任务列表上完成列表任务
    public void UpdateQuestList()
    {
        for (int i = 0; i < Playertaskconter.instance.questList.Count; i++)
        {
            //ui任务信息显示添加到列表里面，
            questuiArry[i].transform.GetChild(0).GetComponent<Text>().text = Playertaskconter.instance.questList[i].questName;
            if (Playertaskconter.instance.questList[i].questStatus==Quest.QuestStatus.Accepted)
            { 
                questuiArry[i].transform.GetChild(1).GetComponent<Text>().text = "<color=red>"+Playertaskconter.instance.questList[i].questStatus+"</color>";
            }
            else if(Playertaskconter.instance.questList[i].questStatus==Quest.QuestStatus.completed)
            {
                questuiArry[i].transform.GetChild(1).GetComponent<Text>().text = "<color=yellow>"+Playertaskconter.instance.questList[i].questStatus+"</color>";
            }
           
        } 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            questpanel.SetActive(!questpanel.activeInHierarchy);
        }
    }
}
