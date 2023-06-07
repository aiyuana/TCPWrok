using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeswp : MonoBehaviour
{

    public GameObject m;
    

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
           
            m.gameObject.SetActive(true);
            Invoke("SS",0.5f);
        }
    }

    void SS()
    {
        m.gameObject.SetActive(false);

            
    }
    
}
