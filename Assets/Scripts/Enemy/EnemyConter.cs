using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyConter : MonoBehaviour
{
    private Animator _animator;
    private float x;
    private float x1=6;
    private float y;
    public GameObject player;
    public GameObject shoot;
    public Transform[] waypoints;//巡逻点位
    public GameObject bullet;
    private static float time=0;
    private  float time2=0;
    public static  int setr = 1;
    private void Awake()
    {
       
        _animator = GetComponentInChildren<Animator> ();
    }

    // Start is called before the first frame update
    void Start()
    {
        
        Invoke("swp",0.1f);
        Invoke("walk",3f);
        
        
        _animator.SetFloat("InputY",1);
        
        
    }
    private void Move()
    {
        
        //获取人物和猪的距离
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (setr == 1)
        {
            transform.LookAt(waypoints[0]);
           
                
            
            
        }

        if (setr==2)
        {
            transform.LookAt(waypoints[1]);
           
           
        }
         
        if (distance < 30)
        {
            stop();
            _animator.SetBool("run",true);
            transform.LookAt(player.transform);
            
        }
        if (10<=distance&&distance < 20)
        {_animator.SetBool("run",false);
            walk();
            if (time>2)
            { _animator.SetBool("fire",true);
                fireshot();
                time = 0;
            }//朝向目标点
            
        }
        
        if (5<=distance&&distance < 10)
        {
           
            _animator.SetFloat("InputY",1);
            stop();
            if (time>3)
            { _animator.SetBool("fire",true);
                fireshot();
                time = 0;
            }
          
        }

        if (distance < 3)
        { 
            _animator.SetBool("fire",false);
            _animator.SetFloat("InputY",-1);
            walk();
        }
        
        else
        {
            walk();
            _animator.SetBool("fire",false);
            
        }
       
        
    }

    void fireshot()
    {
        Vector3 vector3 = new Vector3(shoot.transform.position.x, shoot.transform.position.y, shoot.transform.position.z);
        GameObject clone =  Instantiate(bullet, vector3,transform.rotation);
        Destroy(clone,1.5f);
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.CompareTag("wall"))
        {
            if (setr==2)
            {
                setr = 0;

            }
            setr ++;
            

        }
        
    } 
    private void Update()
    {time2 += Time.deltaTime;
        if (time2<5)
        {
            return;
        }
        Move();
        time += Time.deltaTime;
    }

    void walk()
    {_animator.SetBool("move",true);
        
    }
    void stop()
    {
        _animator.SetBool("move",false);
    }
    public void set()
    {
        x = player.transform.position.x;
        y = player.transform.position.y;
        _animator.SetFloat("InputX",x);
        _animator.SetFloat("InputY",y);
    }
    public void swp()
    {
        _animator.Play("Spawn");
    }
    // Update is called once per frame
    
}
