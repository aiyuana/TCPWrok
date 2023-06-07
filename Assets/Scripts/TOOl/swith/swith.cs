using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//开关触发器
public class swith : MonoBehaviour
{
    private GameObject trigger;
    private GameObject Demo_Scene;
    private GameObject gamecanves;
    private GameObject InputText;
    private  TextMeshProUGUI  _textext;

    void Start()
    {
       Demo_Scene=GameObject.Find("Demo_Scene");
       trigger = Demo_Scene.transform.Find("landpag").gameObject;
       gamecanves=GameObject.Find("gamecanvs");
       InputText = gamecanves.transform.Find("Text (TMP)").gameObject;
       _textext = InputText.GetComponent<TextMeshProUGUI>();
        
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.CompareTag("Player"))
        {
           
           
        }
        if (collider.transform.CompareTag("fring"))
        { 
            trigger.SetActive(true);
         _textext.text = " !!the Elevator is open ";
         

        }
        
    }
    
    
    
}
