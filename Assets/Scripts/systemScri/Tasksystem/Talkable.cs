using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//进入对话，触发npc,任何可以说话的都能说话
public class Talkable : MonoBehaviour
{
    public GameObject door;
    private GameObject zhishi;
    //先判断玩家是否进入范围
    [SerializeField] private bool isEnterd;
    [TextArea(1,5)]
    public string[] lines;

    public Questable questable;
    public GameObject tip;
    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            isEnterd = true;
            zhishi.SetActive(false);
            tip.SetActive(true);
            Invoke("colose",2);
            DialoManage.instance.currentQuestable = questable;
        }

    }

    void colose()
    {
        tip.SetActive(false);
    }
//玩家走了
    public void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            isEnterd = false;
            zhishi.SetActive(true);
            DialoManage.instance.currentQuestable = null;
            
            door.GetComponent<Collider>().enabled = true;
        }
    }

    private void Update()
    {//进入按下空格开始对话
        
        if (isEnterd && Input.GetKeyDown(KeyCode.E))
        {
            DialoManage.instance.ShowDialogue(lines);
        }
    }

    private void Start()
    {
        zhishi = gameObject.transform.Find("Marker 1 Loop").gameObject;
        tip = GameObject.Find("playerUI").transform.Find("TIp").gameObject;
    }
}
