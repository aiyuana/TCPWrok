using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//技能3，释放锁血技能
public class boom : MonoBehaviour
{//敌人激光,吞噬
    
    
    private ParticleSystem part;
    //实例化激光
    private GameObject ran;
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
   
    private GameObject play;
   
    void Start()
    {
        part = GetComponent<ParticleSystem>();
        play=GameObject.FindWithTag("Player");
        ran=GameObject.Find("Projectile 6");
    }
    // void OnParticleCollision(GameObject other)
    // {
    //   
    //   Destroy(play);
    // }
    
    private void OnTriggerStay(Collider collider)
    {
        if (collider.transform.CompareTag("Player"))
        {  
            
            play.transform.GetComponent<PlayerHealth>().enabled = false;
           
        }
    }
}