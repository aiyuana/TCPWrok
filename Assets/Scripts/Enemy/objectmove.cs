using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
//游戏里面机关的移动电梯
public class objectmove : MonoBehaviour
{
    
    [SerializeField] private float movespeed;
    [SerializeField] 
    private float upBoud;
    [SerializeField] 
    private float downBoud;
    [SerializeField] 
    private Vector3 startpos;

    private bool isGameover = false;

  
    private void Start()
    {
        startpos = transform.position;
       
    }

    

    private bool isstart=true;
    private void FixedUpdate()
    {
        if(isGameover) return;
        // transform.Translate(movespeed*Vector3.up*Time.deltaTime);
        //敌人的
        // if (transform.position.y>= leftBoud )
        // {
        //     Destroy(gameObject);
        // }
        if (isstart)
        {
            transform.Translate(movespeed*Vector3.up*Time.deltaTime);
        }

        if (!isstart)
        {
            transform.Translate(movespeed*Vector3.down*Time.deltaTime);
        }
        if(transform.position.y>=upBoud)
        {
            isstart = false;

        }
        else if (transform.position.y <= downBoud)
        {
            isstart = true;

        }
      
    }
    
}


