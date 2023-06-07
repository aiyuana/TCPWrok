using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Spider : MonoBehaviour
{
   
    public GameObject shoot;
    private float x1=1;
    public GameObject bullet;
        private NavMeshAgent agent;//给怪物添加制动巡航组件
        private Animator an;//获取新动画
        public Transform[] waypoints;//创建一个对象数组,把需要导航的位置存入进去
        private int index = 0;
        private float timer = 0;
        private float opentimer = 0;
        private float times = 2;
        private float firtime = 0;
        private Transform player;
        // Use this for initialization
        void Start () {
            agent = GetComponent<NavMeshAgent>();//
            an = GetComponent<Animator>();
            Invoke("look", 3f);
            player = GameObject.FindWithTag("Player").transform;
            Invoke("swp",0.1f);
            an.SetFloat("InputY",x1);
        }

        void look()
        {
            agent.destination = waypoints[index].position;
        }
        
        void walk()
        {an.SetBool("move",true);
        
        }
        void stop()
        {
            an.SetBool("move",false);
        }
        // Update is called once per frame
        void Update () {
            opentimer += Time.deltaTime;
            if (opentimer < 3)
            {  
                return;
                
            }
             
            float dir = Vector3.Distance(player.position, transform.position);//获取玩家距离敌人的距离
            if(dir > 7 && dir < 15)//追踪
            {
                an.SetBool("fire",false);
                transform.LookAt(player.transform);
                walk();
               
                Track();
            }

            if (dir>7&&dir<10)
            {
                an.SetBool("run",false);
                transform.LookAt(player.transform);
                walk();
                Attack();
            }
             if(dir>=5&&dir <= 7)//攻击
            {an.SetFloat("InputY",x1);
                stop();
                an.SetBool("run",false);
                Attack();
            }
             else if (dir<5)
             {
                 an.SetBool("fire",false);
                 an.SetFloat("InputY",-x1);
                 walk();
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
        void fireshot()
        {
            Vector3 vector3 = new Vector3(shoot.transform.position.x, shoot.transform.position.y, shoot.transform.position.z);
            GameObject clone =  Instantiate(bullet, vector3,transform.rotation);
            Destroy(clone,1.5f);
        }
        void Attack()//攻击
        {
            agent.ResetPath();//停止导航
            
            transform.LookAt(player.position);
            if (firtime > 3)
            { 
                an.SetBool("fire",true);
                fireshot();
                firtime = 0;
            }
          
           
        }
        public void swp()
        {
            an.Play("Spawn");
        }
        void Patrol()//自动导航
        {
           
            if (agent.remainingDistance <0.5)//在自动巡航到0.5m后进入这个判断条件
            {
              an.SetBool("run",false);
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
                an.SetBool("run",true);;//播放动画
            }
        }
    }

