using System.Collections;
using Cinemachine;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;
//任务系统,单例模式懒汉，一个工具任务系统全局提醒，玩家遇到不同npc和触发点位触发不同对话
public class DialoManage : MonoBehaviour
{
    public static DialoManage instance;
    private  Transform mUIRoot;
    public Transform root3;
    public CinemachineVirtualCamera cam;
   
    
    //对画窗口
  public GameObject dialogueBox;
    //对话文字
    public Text dialogueText, nameText;
    //对话显示的话，放进一个数组中以免对话过长
    [TextArea(1, 9)] 
   public  string[] dialogueLines;
    private bool isSrolling;
    [SerializeField] private int currentLine;
   public float textseed=3f;

   public Questable currentQuestable;
   
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
    
    private void Start()
    {   EVentcontorl.AddListener(Eventnum.GAMOVer,stop);
        mUIRoot = GameObject.Find("playerUI").gameObject.transform;
        dialogueBox = mUIRoot.transform.Find("Dialogue Panel").gameObject;
        dialogueText = dialogueBox.transform.Find("Dialogue Bavkground").transform.Find("Diag Text").transform.GetComponent<Text>();
        nameText = dialogueBox.transform.Find("Dialogue Bavkground").transform.Find("name Text").transform.GetComponent<Text>();
        dialogueText.text = dialogueLines[currentLine];
        
       
        

    }

    private void Update()
    {//可见的对话即可一句话一句话跳转
        if (dialogueBox.activeInHierarchy)
        {
            if (Input.GetMouseButtonUp(1)&&dialogueText.text==dialogueLines[currentLine])
            {
                if (isSrolling == false)
                {
                    currentLine++;
                    if (currentLine < dialogueLines.Length)
                    {
                        // dialogueText.text = dialogueLines[currentLine];
                        StartCoroutine(ScrollingText());
                    }
                    else
                    {
                        dialogueBox.SetActive(false);
                        cam.Follow = root3;
                        
                        FindObjectOfType<PlayerController>().startgame();
                        if (currentQuestable == null)//委派任务看是否非空，不是npc不能领取
                        {
                            return;
                            
                        }
                        else
                        {
                            currentQuestable.DelegateQuest();//完成任务之后委派任务
                            //需要更新
                            QuestManger.instance.UpdateQuestList();
                        }
                        
                    }
                }
               
            }
        }
       
    }
    private void OnDisable()
    {
        EVentcontorl.RemoveListener(Eventnum.GAMOVer,stop);
    }
    private void stop()
    {
        CancelInvoke();
       
    }
//封装不同人不同npc的对话
    public void ShowDialogue(string[] _newLines)
    {
        if (dialogueBox.activeInHierarchy == false)
        {
            dialogueLines = _newLines;
            currentLine = 0;
            // dialogueText.text = dialogueLines[currentLine];//一行行输出
            //携程函数
            StartCoroutine(ScrollingText());
            
            dialogueBox.SetActive(true);
            FindObjectOfType<PlayerController>().stopgame();
        }
      
    }
    //在文字没有出来前不让玩家
    private IEnumerator ScrollingText()
    {
        isSrolling = true;
        dialogueText.text = "";
        foreach (char letter in dialogueLines[currentLine].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textseed*Time.deltaTime);
        }
        
        
        
        isSrolling = false;
    }
}
