using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class daoenmy : MonoBehaviour
{
   
   
    private float x1=1;
    
        private NavMeshAgent agent;//给怪物添加制动巡航组件
        private Animator an;//获取新动画
        public Transform[] waypoints;//创建一个对象数组,把需要导航的位置存入进去
        private int index = 0;
        private float timer = 0;
        private float opentimer=0;//开场动画时间
        private float times = 2;
        private float firtime = 0;
        private Transform player;
        // Use this for initialization
        void Start () {
            
            
            
            Invoke("swp",0.1f);
            Invoke("look",3f);
            agent = GetComponent<NavMeshAgent>();//
            an = GetComponent<Animator>();
           
            player = GameObject.FindWithTag("Player").transform;
            
            
            an.SetFloat("InputY",x1);
        }

        void look()
        {
            agent.destination = waypoints[index].position;
        }
        void walk()
        {an.SetBool("run",true);
        
        }
        void stop()
        {
            an.SetBool("run",false);
        }
        // Update is called once per frame
        void Update ()
        {
            
            opentimer += Time.deltaTime;
            if (opentimer<2)
            return;
            
            float dir = Vector3.Distance(player.position, transform.position);//获取玩家距离敌人的距离
            if(dir >= 10 && dir < 15)//追踪
            {
                an.SetBool("fire",false);
                transform.LookAt(player.transform);
                walk();
               
                Track();
            }

            if (dir>=0.8&&dir<10)
            {an.SetBool("fire",false);
                an.SetBool("run",false);
                transform.LookAt(player.transform);
                an.SetBool("move",true);
              
            }
             if(dir <0.8)//攻击
             {
stop();
an.SetBool("move",false);
                 Attack();
             }
            
             
            else
            { an.SetBool("fire",false);
                stop();
                
                
                    Patrol();
                
               
            }

            firtime += Time.deltaTime;
        }
        void Track()
        {
            //transform.LookAt(player.position);//给定条件看向玩家   这行代码可以不用
            agent.SetDestination(player.position);//自动导航到玩家的位置
            
        }
        
        void Attack()//攻击
        {
            agent.ResetPath();//停止导航
            
            transform.LookAt(player.position);
            if (firtime > 2)
            { 
                an.SetBool("fire",true);
                
                firtime = 0;
            }
          
           
        }
        public void swp()
        {
            an.Play("Spawn");
        }
        void Patrol()//自动导航
        {
            // if(agent.isOnNavMesh)
            // {
            //     agent.Warp(gameObject.transform.position);
            // }
          
            if (agent.remainingDistance < 0.5f)//在自动巡航到0.5m后进入这个判断条件
            {
              an.SetBool("move",false);
                timer += Time.deltaTime;
                if (timer >= times)
                {
                    timer = 0;
                    index++;
                    index %= 4;//给怪物巡逻几个点位就给几
                    agent.SetDestination(waypoints[index].position);//继续网下一个位置导航
                }
            }
            else
            {
                an.SetBool("move",true);;//播放动画
            }
        }
    }

