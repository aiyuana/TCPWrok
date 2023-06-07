
using TMPro;

using Unity.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
//角色升级系统设置角色不同等级不同血量和每次获得经验的增长情况，每个等级所要获得的经验不一样，并播放不同音效
public class LevelSystem : MonoBehaviour
{
    public int level;
    public float maxLevel;
    public float currentXp;
    public int nextLevelXp = 100;
    [Header("Multipliers")]
    [Range(1f, 300f)]
    public float additionMultiplier;
    [Range(2f, 4f)]
    public float powerMultiplier = 20f;
    [Range(7f, 14f)]
    public float divisionMultiplier = 7f;
    public GameObject levelUpEffect;

    [Header("UI")]
    private Transform mUIRoot;
    public Image frontXpBar;
    public Image backXpBar;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI XpText;
    private Text uesr;

    public GameObject player;
    //Audio  
    [Header("Audio")]
    public AudioClip levelUpSound;
    private AudioSource source;
    //Timers
    private float lerpTimer;
    private float delayTimer;

    void Start()
    {  mUIRoot = GameObject.Find("playerUI").transform;
        uesr = mUIRoot.Find("name").GetComponent<Text>();
        levelText.text = "Level " + level;
        level = 1;
        
        PlayerPrefs.SetInt("lever",level);
        XpText.text = Mathf.Round(currentXp) + "/" + Mathf.Round(nextLevelXp);
        frontXpBar.fillAmount = currentXp / nextLevelXp;
        backXpBar.fillAmount = currentXp / nextLevelXp;
        nextLevelXp = CalculateNextLevelXp();
        source = GetComponent<AudioSource>();
    }

    void Update()
    {//当一级经验满了增加一级，传输等级
        UpdateXpUI();
        if (level != maxLevel)
        {
            if (currentXp >= nextLevelXp)
            {
                LevelUp();
                // player.transform.GetComponent<PlayerHealth>().maxuphealth(100);
            }        
        }
        else
        {
            currentXp = nextLevelXp;
            XpText.text = "MAX";
            frontXpBar.fillAmount = currentXp / nextLevelXp;
            backXpBar.fillAmount = currentXp / nextLevelXp;
        }
    }
    //将值给到ui
    private void UpdateXpUI() 
    {
        float xpFraction = currentXp / nextLevelXp;
        float fXP = frontXpBar.fillAmount;

        if (fXP < xpFraction)
        {
            delayTimer += Time.deltaTime;
            backXpBar.fillAmount = xpFraction;
            if (delayTimer > 3)
            {
                lerpTimer += Time.deltaTime;
                float percentComplete = lerpTimer / 5;
                percentComplete = percentComplete * percentComplete;
                frontXpBar.fillAmount = Mathf.Lerp(fXP, backXpBar.fillAmount, percentComplete);
            }

        }
        XpText.text = currentXp + "/" + nextLevelXp;
    }

    public void GainExperienceFlatRate(float xpGained)
    {
            currentXp += xpGained;
            lerpTimer = 0f;
            delayTimer = 0f;
    }
    //获得经验看人物角色以及经验
    public void GainExperienceScalable(float xpGained, int passedLevel)
    {
        if (passedLevel < level)
        {
            float multiplier = 1 + (level - passedLevel) * 0.1f;
            currentXp += Mathf.Round(xpGained*multiplier);

        }
        else
        {
            currentXp += xpGained;

        }

        lerpTimer = 0f;
        delayTimer = 0f;

    }
    //等级提升
    public void LevelUp() 
    {
        level += 1;
        backXpBar.fillAmount = 0f;
        frontXpBar.fillAmount = 0f;
        currentXp = Mathf.Round(currentXp-nextLevelXp);

        nextLevelXp = CalculateNextLevelXp();
        level = Mathf.Clamp(level,0, 50);

        XpText.text = Mathf.Round(currentXp) + "/" + nextLevelXp;
        levelText.text = "Level " + level;

        // Instantiate(levelUpEffect, transform.position, Quaternion.identity);
      
        GetComponent<PlayerHealth>().IncreaseHealth(level);
        Debug.Log(level);
        Debug.Log(uesr.text);
        sqlstats.Instance.selectlever(new[] {uesr.text, level.ToString()});
       
    }
    private void DisplayAccrueAmount() 
    {
        
    }
    //计算下一个等级所要的经验
    private int CalculateNextLevelXp() 
    {
        int solveForRequiredXp = 0;
        for (int levelCycle = 1; levelCycle <= level; levelCycle++)
        {
            solveForRequiredXp += (int)Mathf.Floor(levelCycle + additionMultiplier * Mathf.Pow(powerMultiplier, levelCycle / divisionMultiplier));
        }
        return solveForRequiredXp / 4;
    }
}
