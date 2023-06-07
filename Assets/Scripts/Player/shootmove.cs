using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootmove : MonoBehaviour
{

 

    private Rigidbody rigidbody;//变量定义：开辟空间
    public  float speed=-1;
    private float m_MouseSensitivity= 2.0f;//控制鼠标敏感度
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        
    }
 
    // Update is called once per frame
    //Update 不能不保证固定时间，不同配置给出反馈时间不同
    //FixedUpdate是没0.02秒调用一次
 
    void FixedUpdate()
    {
 
        float vertical = Input.GetAxis("Mouse Y");
        float horizontal = Input.GetAxis("Mouse X");    
        //第一种
        transform.Rotate(vertical * -m_MouseSensitivity, horizontal * m_MouseSensitivity, 0);
        //第二种
        //第一人称扭头代码
        //四元数公式是以乘积的方式进行，如果都是四元数t*p，优先执行t后执行p
        //var rotation = transform.rotation * Quaternion.AngleAxis(horizontal*m_MouseSensitivity,Vector3.up)
        //*Quaternion.AngleAxis(vertical * m_MouseSensitivity, Vector3.left);
        //transform.rotation = rotation;
    }
}
    

