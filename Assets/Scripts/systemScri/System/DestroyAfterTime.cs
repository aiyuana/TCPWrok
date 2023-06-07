using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
//销毁物品
public class DestroyAfterTime : MonoBehaviour
{
    public float destroyAfter;

    void Update()
    {
        Destroy(gameObject, destroyAfter);
    }
}
