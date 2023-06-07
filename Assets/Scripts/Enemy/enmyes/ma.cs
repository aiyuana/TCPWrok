using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//敌人被生出来朝着主角并对主角进行攻击，特效粒子里面带的碰撞体对人物检测并造成伤害
public class ma : MonoBehaviour
{
    //敌人激光
    
    
    private ParticleSystem part;
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
    private GameObject boom;
    private GameObject play;
    private GameObject enmy;
    private GameObject enmy1;
   
    void Start()
    {
        part = GetComponent<ParticleSystem>();
        play=GameObject.FindWithTag("Player");
        enmy=GameObject.FindWithTag("gun");
        enmy1=GameObject.FindWithTag("dama");
        boom = play.transform.Find("Magic circle 7").gameObject;
        
    }
    void OnParticleCollision(GameObject other)
    {
        if (other.name=="Character_CyborgNinja_01")
        {
            enmy1.transform.GetComponent<Enmyhp>().Deamag(5);
        }

        else
        
        {
            enmy.transform.GetComponent<Enmyhp>().Deamag(5);
        }
        
    }
}