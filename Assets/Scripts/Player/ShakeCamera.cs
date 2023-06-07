using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class ShakeCameraData
{
    //震屏类型
    public float shakeType = 0;//0--震屏,1--拉近拉远
    //延迟时间
    public float dealy = 0f;
    //持续时间（秒）
    public float duration = 0f;
    //速度（每帧增量）
    public float power = 0f;
}
public class ShakeCamera : MonoBehaviour
{
	public Queue<float[]> paramQueue=new Queue<float[]>();


    public List<ShakeCameraData> List = new List<ShakeCameraData>();
    bool isPlaying=false;
	bool isShake=false;

    private float shakeType = 0;
	private float dealy=0f;
    //持续时间
    private float duration = 0f;
    //强度
    private float power = 0f;


    private Vector3 orgPos;
	private Vector3 deltaPos = Vector3.zero;

	private void Update()
	{
		DebugTest();
	}

	public void DebugTest()
    {
        for (int i = 0; i < List.Count; i++)
        {
            Play(List[i].shakeType,List[i].dealy, List[i].duration, List[i].power);
        }

    }
	public void Play(float shakeType,float dealy,float duration,float power)
	{
		paramQueue.Enqueue (new float[]{ shakeType, dealy, duration,power});
		if (!isPlaying) {
			this.enabled = true;
			orgPos = transform.position;
			StartCoroutine (DoUpdate());
		}
	}
	public void Stop()
	{
		this.enabled = false;
	}

	public IEnumerator DoUpdate()
	{
		isPlaying = true;
		while (paramQueue.Count>0) {
	
			float[] param = paramQueue.Dequeue ();
            shakeType = param[0];
            dealy = param [1];
			duration = param [2];
			power = param [3];

            isShake = false;

            transform.position = orgPos;
            deltaPos = Vector3.zero;

            if (dealy > 0.05f) {
				yield return new WaitForSeconds(dealy);
			}
			if (duration > 0.05f) {
              
                isShake = true;
				yield return new WaitForSeconds(duration);
			}
		}
		Stop ();
	}
		
    // Update is called once per frame
    void LateUpdate ()
	{
		if(this.isShake)
        {
            switch (shakeType)
            {
                case 0: //随机震屏
                    transform.position -= deltaPos;
                    deltaPos = Random.insideUnitSphere * power;
                    transform.position += deltaPos;
                    break;
                case 2://横向震屏
                    transform.position -= deltaPos;
                    deltaPos = transform.right.normalized * power;
                    power = -power;
                    transform.position += deltaPos;
                    break;
                case 3://纵向震屏
                    transform.position -= deltaPos;
                    deltaPos = transform.up.normalized * power;
                    power = -power;
                    transform.position += deltaPos;
                    break;
                default:
                    break;
            }
        }
    }

	void OnDisable()
	{
        isPlaying = false;
        isShake = false;
        transform.position = orgPos;
        deltaPos = Vector3.zero;
        paramQueue.Clear();
    }

}



