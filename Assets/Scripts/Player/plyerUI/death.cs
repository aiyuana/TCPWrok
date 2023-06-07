using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//人物掉落到不符合的地方直接对人物扣血到200
public class death : MonoBehaviour
{
 private void OnTriggerStay(Collider collider)
    {
        if (collider.transform.CompareTag("Player"))
        {
            collider.transform.GetComponent<PlayerHealth>().TakeDamage(200);
        }
        
    }
}
