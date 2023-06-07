using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Playables;

public class opendoor : MonoBehaviour
{//切换敌人生成
    //门的动画机和携程开关门
    public Transform root;
    public Transform root1;
    public Transform root2;
    public CinemachineVirtualCamera cam;
    public GameObject leftdrool;
    public GameObject rightdrool;
    public GameObject enmy;
    public float duration = 1f;
    public string[] lines;// 平移时间
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator MoveToPosition(Vector3 targetPosition, float time)
    {
        float elapsedTime = 0;
        Vector3 startingPos = leftdrool.transform.position;
        
        while (elapsedTime < time)
        {
            leftdrool.transform.position = Vector3.Lerp(startingPos, targetPosition, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        leftdrool.transform.position = targetPosition;
    }
    IEnumerator MoveToPosition1(Vector3 targetPosition, float time)
    {
        float elapsedTime = 0;
        Vector3 startingPos = rightdrool.transform.position;
        
        while (elapsedTime < time)
        {
            rightdrool.transform.position = Vector3.Lerp(startingPos, targetPosition, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        rightdrool.transform.position = targetPosition;
    }

   public void opend()
    {
        StartCoroutine(MoveToPosition(leftdrool.gameObject.transform.position + new Vector3(10f, 0f, 0f), duration));//开门
        StartCoroutine(MoveToPosition1(rightdrool.gameObject.transform.position + new Vector3(-10f, 0f, 0f), duration));
    }
   public void OnTriggerEnter(Collider other)
   {
        
       if (other.transform.CompareTag("Player"))
       {
           opend();
           cam.Follow = root;
           leftdrool.GetComponent<PlayableDirector>().enabled = true;
           
           Invoke("rot",9.5f);
           Invoke("talk",9.5f);
           Invoke("rot2",13f);
           gameObject.GetComponent<Collider>().enabled = false;

       }
   }
void talk()
{
    DialoManage.instance.ShowDialogue(lines);
}
   void rot2()
   {
       cam.Follow = root2;
   }
   void rot()
   {//切换相机的跟踪
       cam.Follow = root1;
       enmy.gameObject.SetActive(true);
   }
}
