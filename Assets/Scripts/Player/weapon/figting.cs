using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Cinemachine;
using StarterAssets;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UIElements;

//武器脚本
public class figting : MonoBehaviour
{ public Transform root3;
    public CinemachineVirtualCamera cam;
    private  Animator _animator;
    public GameObject box;
    public float emXp=50;
    public int PLevel=10;
    private GameObject player;
    LevelSystem playerLevel;
    private Enmyhp _enmyhp;
    public static int num = 0;
    public string[] lines;
    private GameObject winplane;
    private GameObject uiroot;
    public GameObject cans;
    private void Start()
    { uiroot = GameObject.Find("playerUI");
        winplane = uiroot.transform.Find("Win").gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        playerLevel = player.GetComponent<LevelSystem>();
        
    }

    public  void OnDead() 
    {
     
        playerLevel.GainExperienceScalable(emXp, PLevel);
       
    }

    void playerovergame()
    {
        DialoManage.instance.ShowDialogue(lines);  
        Invoke("win",6f);
    }

    void win()
    {
        winplane.SetActive(true);
    }
//攻击敌人
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.CompareTag("Enemy"))
        {
           
          Destroy(collider.gameObject);
          OnDead();
        }
        if (collider.transform.CompareTag("gun"))
        {
           collider.GetComponent<Enmyhp>().Deamag(5);
           collider.GetComponent<Animator>().Play("HitReact1");
           CA.instan.fu();
          
        }
        
        if (collider.transform.CompareTag("dama"))
        {
            collider.GetComponent<Enmyhp>().Deamag(5);
            collider.GetComponent<Animator>().Play("HitReact1");
            CA.instan.fu();
          
        }

        if (collider.transform.CompareTag("bossgun"))
        {
            Destroy(collider.gameObject);
            num = num + 1;
            if (num==4)
            {
                box.GetComponent<Rigidbody>().useGravity = true;
                CA.instan.fu3();
            }
        }

        if (collider.transform.CompareTag("boss"))
        {
            cam.Follow = root3;
            box.GetComponent<PlayableDirector>().enabled = true;
            CA.instan.fu4();
            Destroy(collider.gameObject,5f);
            Destroy(cans.gameObject,5f);
            
            Invoke("playerovergame",6f);
            Invoke("playerskil",3f);
        }
    }

    void playerskil()
    {
        box.transform.Find("Skill 7").gameObject.SetActive(true);
    }
}
