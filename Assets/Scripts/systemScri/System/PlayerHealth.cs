using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
//玩家的血量回血扣血的机制
public class PlayerHealth : MonoBehaviour
{
    public AudioClip hut2;
    public GameObject camer;
    public float health;
    private float lerpTimer;
    public float maxHealth ;
    public float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image backHealthBar;
    public TextMeshProUGUI healthText;
    private GameObject player;
//死亡特效
    private GameObject dea;
    // Start is called before the first frame update
    private Animator _animator;
    void Start()
    {
        maxHealth = 250;
        health = maxHealth;
        _animator = GetComponent<Animator>();
        camer = GameObject.Find("PlayerFollowCamera");
    }
    public void maxuphealth(float ll)
    {
        maxHealth +=ll*Time.deltaTime;
       
    }
    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        maxHealth=Mathf.Clamp(maxHealth, 0, maxHealth);
        UpdateHealthUI();
        death();

    }
   
    void death()
    {
        if (health==0)
        {
            _animator.SetBool("die",true);
         
            EVentcontorl.BroadCast(Eventnum.GAMOVer);
            
        }
    }
    public void UpdateHealthUI() 
    {
      //  Debug.Log(health);
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;
        if (fillB > hFraction) 
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        if (fillF < hFraction) 
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
        }
            healthText.text = health + "/" + maxHealth;
    }


    public void TakeDamage(float damage) 
    {
        if (health==0)
        {
            return;
            
        }
        CA.instan.fu();
        
        Debug.Log("sss1");
        _animator.SetBool("hut",true);
        health -= damage;
        lerpTimer = 0f;
      Invoke("hut",0.3f);
    }

    void hut()
    {
        _animator.SetBool("hut",false);
        AudioManager.instance.AudioPlay(hut2);
    }
    public void RestoreHealth(float healAmount) 
    {
        health += healAmount*Time.deltaTime;
        lerpTimer = 0f;
    }


    public void IncreaseHealth(int level)
    {
        maxHealth += Mathf.RoundToInt((health * 0.01f) * ((100 - level) * 0.1f));
        health = maxHealth;
    }
    public void GameOver() { }

}
