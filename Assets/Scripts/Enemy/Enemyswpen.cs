using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyswpen : MonoBehaviour
{
    public GameObject enmy;
    private GameObject Player;
    public Transform pos;//位置

    void Start()
    {
        EVentcontorl.AddListener(Eventnum.GAMOVer,GameOver);
    }

    private void OnDisable()
    {
        EVentcontorl.RemoveListener(Eventnum.GAMOVer,GameOver);
    }
    void GameOver()
    {
       gameObject.transform.GetComponent<Collider>().enabled = false;
       
 
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player") {
            Vector3 pos = new Vector3(-21, 25, 90);
          
       

            Instantiate(enmy, pos, transform.rotation);
           
           
        }


    }
    
    
    
}
