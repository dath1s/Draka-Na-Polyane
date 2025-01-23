using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ExpManager : MonoBehaviour
{
    public int level;
    public int currentExp;
    public int expToLevel = 10;
    public float expGrowthMultiplier = 1.1f;
    public Slider expSlider;
    public TMP_Text currentLevelText;

    public delegate void LevelIncreased();
    public static event LevelIncreased OnLevelIncreased;


    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GainExpirience(2);
        }
    }

    private void OnEnable()
    {
        Enemy_Health.OnEnemyDefeated += GainExpirience;
    }

    private void OnDisable()
    {
        Enemy_Health.OnEnemyDefeated -= GainExpirience;
    }

    public void GainExpirience(int amount)
    {
        currentExp += amount;

        if (currentExp > expToLevel)
        {
            for (int i = 0; i < currentExp / expToLevel; i++)
                LevelUp();
        }

        UpdateUI();
    }

    private void LevelUp()
    {
        level++;
        currentExp -= expToLevel;
        expToLevel = Mathf.RoundToInt(expToLevel * expGrowthMultiplier);

        OnLevelIncreased();
    }

    public void UpdateUI()
    {
        expSlider.maxValue = expToLevel;
        expSlider.value = currentExp;
        currentLevelText.text = "Level: " + level;


    }
}
