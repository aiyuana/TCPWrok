using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//剑的特效
public class playeratarUI : MonoBehaviour
{
    public int num = 0;
    private GameObject attack;
        public void Start()
        {
           
            attack= Resources.Load<GameObject>("Slash12");

        }
    
        void Update()
        {

            if (Input.GetMouseButton(0))
            {
                GameObject btn = EventSystem.current.currentSelectedGameObject;
                if (btn == null)
                {
                    if (num == 0)
                    {
                        GameObject clone = Instantiate<GameObject>(attack);
                        clone.transform.position = transform.position;
                        Destroy(clone, 0.3f);
                        num = num + 1;
                    }
                    
                }
                
            }
            else
            {
                num = 0;
            }


        }
}
