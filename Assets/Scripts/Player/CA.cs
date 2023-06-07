using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CA : MonoBehaviour
{
    public static CA instan;
    CinemachineVirtualCamera virtualCamera;
    CinemachineBasicMultiChannelPerlin noise;
    void Start()
    {
        instan = this;
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        
    }

   public void fu()
    {
       
 Debug.Log("ss");
        noise.m_AmplitudeGain = 1; //震动个数
        noise.m_FrequencyGain = 8; //震动幅度
        Invoke("fu1",0.3f);
    }
   public void fu3()
    {
        noise.m_AmplitudeGain = 2; //震动个数
        noise.m_FrequencyGain = 20; //震动幅度
        Invoke("fu1",3f);
    }
   public void fu4()
    {
        noise.m_AmplitudeGain = 2; //震动个数
        noise.m_FrequencyGain = 20; //震动幅度
        Invoke("fu1",5f);
    }
   

  public  void fu1()
    {
        noise.m_AmplitudeGain = 0; //震动个数
        noise.m_FrequencyGain = 0; //震动幅度
    }
}

