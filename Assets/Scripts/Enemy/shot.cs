using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//实例化碰撞对人物造成伤害
public class shot : MonoBehaviour
{
    private GameObject play;
    void Start()
    {
        play=GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnParticleCollision(GameObject other)
    {
        
       play.transform.GetComponent<PlayerHealth>().TakeDamage(10);
    }

}
