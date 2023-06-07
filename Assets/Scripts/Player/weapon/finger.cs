using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
//攻击动画检测，并且对武器加触发器变成开启状态
public class finger : MonoBehaviour
{
    private GameObject Dao;
   
    private AudioSource source;
    private Animator  _animator ;
    public GameObject gen;
    public int TI=0;
  private void Update()
   {
       
       AnimationClick(); 
   }
  
  private void Start()
  { 
      _animator = gameObject.transform.GetComponent<Animator>();
     
  //  Dao=gameObject.transform.Find("Finger_01 1").transform.Find("fring").gameObject;
 Dao=gen.transform.Find("fring").gameObject;
     
       
  }
  
   private void AnimationClick( )
   {
       if (gameObject.GetComponent<PlayerController>().weaint == true)
       {



           if (_animator.GetCurrentAnimatorStateInfo(1).IsName("Atk8") ||
               _animator.GetCurrentAnimatorStateInfo(0).IsName("atk_5")||_animator.GetCurrentAnimatorStateInfo(1).IsName("Attack_C2_1"))
           {

               Dao.transform.GetComponent<Collider>().enabled = true;
               if (TI==0)
               {
                   Dao.transform.GetComponent<AudioSource>().Play();
                   TI = TI + 1;
               }

               

           }
           else
           {
               TI = 0;
               Dao.transform.GetComponent<Collider>().enabled = false;

           }
       }

   }
}
