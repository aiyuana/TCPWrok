using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chagecoth : MonoBehaviour
{
    //按下鼠标经行换装
    // Start is called before the first frame update
    public Button right;
    public Button left;
    public Transform StartPsnel;
    public Transform BtnPanel;
    int index = 0;
    void Start()
    {
        BtnPanel.GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
        {
           
            //向左边选择角色 三元运算符

            index = index.Equals(0) ? 19 : --index;
            ShowPlayer(index);
        });
        BtnPanel.GetChild(1).GetComponent<Button>().onClick.AddListener(() =>
        {
            index = index.Equals(19) ? 0 : ++index;
            //向右边选择角色
            ShowPlayer(index);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.transform.CompareTag("Player"))
        {
            BtnPanel.gameObject.SetActive(true);
           
        }
            
    }
    private void OnTriggerExit(Collider collider)
    {
        if(collider.transform.CompareTag("Player"))
        {
            BtnPanel.gameObject.SetActive(false);
           
        }
            
    }
    private void ShowPlayer (int value)
    {
        for(int i = 0; i < StartPsnel.childCount; i++)
        {
            StartPsnel.GetChild(i).gameObject.SetActive(false);
        }
        StartPsnel.GetChild(value).gameObject.SetActive(true );
    }
}
