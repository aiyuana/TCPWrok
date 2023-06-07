using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Playables;
public class talkboos : MonoBehaviour
{  
    public Transform root2;
    public Transform root3;
    public CinemachineVirtualCamera cam;
    public string[] lines;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.CompareTag("Player"))
        {
            CA.instan.fu3();
            cam.Follow = root2;
            DialoManage.instance.ShowDialogue(lines);  
            gameObject.GetComponent<PlayableDirector>().enabled = true;
           Invoke("playersond",5f);
           GameObject.FindWithTag("Player").GetComponent<Animator>().Play("ideo");
           Invoke("rot",20f);
           
        }
        
    }

    void playersond()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }

    void rot()
    {
        cam.Follow = root3;
    }
}
    

