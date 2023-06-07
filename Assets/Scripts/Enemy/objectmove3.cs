using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//机关3
public class objectmove3 : MonoBehaviour
{
    // private Rigidbody _rigidbody;
    // public float force = 20;
    // private Vector3 dir=Vector3.up;
    // void Start()
    // {
    //     _rigidbody = GetComponent<Rigidbody>();
    //    
    // }
    //
    // // Update is called once per frame
    // void FixedUpdate()
    // {
    //     _rigidbody.AddForce(Vector3.up*force);
    // }
    public float speed = 10;
    private Rigidbody _rigidbody;
    // 移动方向
    private Vector3 dir = Vector3.up;
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        var positon = dir * (speed * Time.deltaTime);
        _rigidbody.MovePosition(transform.position + positon);
    }

    
}
