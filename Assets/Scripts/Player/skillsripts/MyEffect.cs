using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using  UnityEngine.UI;
//技能释放的面板触控选择，代理模式，
public class MyEffect : MonoBehaviour, IPointerClickHandler
{

    [SerializeField] private float cd = 2;

    [SerializeField] SKillSortEnnum sKillSort;

    private GameObject player;
    private Image _mask;
    private Text _time;

    public void OnPointerClick(PointerEventData eventData)
    {
        USeSkill();
    }
//一键释放所有技能
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            
            
                USeSkill();
            
           
        }
    }
    void USeSkill()
    {

        if (_mask.fillAmount != 0)
        {
            return;
        }
        StartCoroutine(UseSkillcor());
        playeffect.Instance.UseSkill(sKillSort);
    }

    IEnumerator UseSkillcor()
    {
        float workTime = 0;
        while (true)
        {
            workTime += Time.deltaTime;
            _mask.fillAmount = Mathf.Lerp(1, 0, workTime / cd);
            _time.text = (cd - workTime).ToString("f1");
            _time.color=Color.Lerp(Color.red, Color.green, workTime/cd);
            if (workTime>4)
            {
                playeffect.Instance.stop(sKillSort);
                player.transform.GetComponent<PlayerHealth>().enabled = true;
            }
            if (workTime/cd>=1)
            {
                _time.text = "";
                
                break;
                
            }
            
            
            yield return null;
        }
    }

    void Start()
    {
        _mask = transform.Find("Image").GetComponent<Image>();
        _time = transform.Find("Text").GetComponent<Text>();
        player=GameObject.FindWithTag("Player");
    }
}