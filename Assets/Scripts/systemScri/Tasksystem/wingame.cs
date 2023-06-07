using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wingame : MonoBehaviour
{

    private GameObject winplane;
    private GameObject uiroot;

    private void Start()
    {
        uiroot = GameObject.Find("playerUI");
        winplane = uiroot.transform.Find("Win").gameObject;
    }
    public void OnTriggerExit(Collider other)
    {
        
        if (other.transform.CompareTag("Player"))
        {
           winplane.SetActive(true);
        }
    }
}
