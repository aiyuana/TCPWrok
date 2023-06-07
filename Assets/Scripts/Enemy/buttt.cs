using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//最开始准备用的实例化子弹后面改成激光了
public class buttt : MonoBehaviour
{  private GameObject player;
    LevelSystem playerLevel;
    public int enemyLevel;
    public float enemyXp;
    public float XpMultiplier;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerLevel = player.GetComponent<LevelSystem>();
        enemyLevel = Random.Range(1, playerLevel.level + 2);
        enemyXp = Mathf.Round(enemyLevel * 6 * XpMultiplier);
       
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.CompareTag("Player"))
        {
            collider.transform.GetComponent<PlayerHealth>().TakeDamage(10);
            // Destroy(gameObject);
            OnDead();
        }
        
    }  public  void OnDead() 
    {
       
        playerLevel.GainExperienceScalable(enemyXp, enemyLevel);
       
    }
}
