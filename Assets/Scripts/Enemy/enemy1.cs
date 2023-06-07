using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
//敌人控制器，对玩家的扣血和朝着玩家运动
public class enemy1 : MonoBehaviour
{   private GameObject player;
    LevelSystem playerLevel;
    public int enemyLevel;
    public float enemyXp;
    public float XpMultiplier;
    private float timer;
    // public GameObject spriteGO;
    private float shootInterval;
    
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.CompareTag("Player"))
        {
            collider.transform.GetComponent<PlayerHealth>().TakeDamage(5);
            // Destroy(gameObject);
            OnDead();
            
        }
        
    }
   
    private void  Update()
    {
        Move();
    }

    private void Move()
    {
        //获取人物和猪的距离
        float distance = Vector3.Distance(player.transform.position, transform.position);
        transform.LookAt(player.transform); //朝向目标点
        if (distance < 30&&distance>3)
        {
          
            transform.Translate(Vector3.forward * 3f * Time.deltaTime);
        }
        
    }

    void gameover()
    {
        gameObject.SetActive(false);
        
    }
    private void Start()
    {
        EVentcontorl.AddListener(Eventnum.GAMOVer,gameover);
        player = GameObject.FindGameObjectWithTag("Player");
        playerLevel = player.GetComponent<LevelSystem>();
        enemyLevel = Random.Range(1, playerLevel.level + 2);

        //Scale Enemy XP ----- don't use this if you want to set enemy levels manually.
        enemyXp = Mathf.Round(enemyLevel * 6 * XpMultiplier);
       
    }
    private void OnDisable()
    {
        EVentcontorl.RemoveListener(Eventnum.GAMOVer, gameover);
    }
     public  void OnDead() 
    {
       
        playerLevel.GainExperienceScalable(enemyXp, enemyLevel);
       
    }
}
