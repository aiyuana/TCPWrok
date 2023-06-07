using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Enmyhp : MonoBehaviour
{

//敌人控制器，对玩家的扣血和朝着玩家运动
  private GameObject player;
    LevelSystem playerLevel;
    public int enemyLevel;
    public float enemyXp;
    public float XpMultiplier;
    private float timer;
    // public GameObject spriteGO;
    private float shootInterval;
    public int HP = 20;
    private void  Update()
    {
        
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

    public void Deamag(int a)
    {
        HP = HP - a;
        CA.instan.fu();
        if (HP<=0)
        {
            Death();
        }

    }

   public void Death()
    {
        Destroy(gameObject);
        OnDead();
    }
}

