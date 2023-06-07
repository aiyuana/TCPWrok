using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 回血脚本
/// </summary>
public class health : MonoBehaviour
{
    private void OnTriggerStay(Collider collider)
    {
        if (collider.transform.CompareTag("Player"))
        {
            collider.transform.GetComponent<PlayerHealth>(). RestoreHealth(8f);
        }
    }
}
