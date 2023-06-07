using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class musicconter : MonoBehaviour
{
//主声音和玩家声音
    public AudioSource mainadio;

    public AudioSource playerdio;
    public Image scen;
    public Slider mainadioslider;

    public Slider playerdiosilder;
    public Slider scensilder;
    // Start is called before the first frame update
    void Start()
    {
       // playerdio.volume = GlobalAudioManager.Instance().GetEFF_Volume();
    }

    // Update is called once per frame
    void Update()
    {
        mainadio.volume = mainadioslider.value;
        playerdio.volume = playerdiosilder.value;
        scen.GetComponent<Image>().color =
            new Color(0, 0, 0, (float) ((1 - scensilder.value) * 0.7));
    }
}
