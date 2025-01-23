using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public TMP_Text healthText;
    public Animator healthTextAnim;
    public float healthGrowthMultiplier = 1.2f;


    private void Start()
    {
        healthText.text = "HP:" + StatsManager.Instance.currentHealth + "/" + StatsManager.Instance.maxHealth;
    }

    private void OnEnable()
    {
        ExpManager.OnLevelIncreased += LevelBonus;
    }

    private void OnDisable()
    {
        ExpManager.OnLevelIncreased -= LevelBonus;
    }

    public void ChangeHealth(int amount)
    {
        StatsManager.Instance.currentHealth += amount;
        healthTextAnim.Play("TextUpdate");

        healthText.text = "HP:" + StatsManager.Instance.currentHealth + "/" + StatsManager.Instance.maxHealth;
        if (StatsManager.Instance.currentHealth <= 0) {
            gameObject.SetActive(false);
        }
    }

    private void LevelBonus()
    {
        IncreaseMaxHealth();
        RecoverHealth();
    }

    public void RecoverHealth()
    {
        int healthDelta = StatsManager.Instance.maxHealth - StatsManager.Instance.currentHealth;
        ChangeHealth(healthDelta);
    }

    public void IncreaseMaxHealth()
    {
        StatsManager.Instance.maxHealth = Mathf.RoundToInt(StatsManager.Instance.maxHealth * healthGrowthMultiplier);
    }

}
