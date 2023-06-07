using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//敌人被生出来朝着主角并对主角进行攻击，特效粒子里面带的碰撞体对人物检测并造成伤害
public class ParticleCollisionInstance : MonoBehaviour
{
    //敌人激光
    
    
    private ParticleSystem part;
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
    private GameObject boom;
    private GameObject play;
    private GameObject enmy;
   
    void Start()
    {
        part = GetComponent<ParticleSystem>();
        play=GameObject.FindWithTag("Player");
        enmy=GameObject.FindWithTag("gun");
        boom = play.transform.Find("Magic circle 7").gameObject;
        
    }
    void OnParticleCollision(GameObject other)
    {
        //当开启保护技能时无法对主绝扣血
        if (boom.activeInHierarchy ==false)
        {
            play.transform.GetComponent<PlayerHealth>().TakeDamage(10);
        }
            
    }
}