using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private int maxHealth;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        healthText.text = $"HP: {currentHealth}";
    }

    public void TakeDamage()
    {
        currentHealth--;
        healthText.text = $"HP: {currentHealth}";

        if (currentHealth <= 0)
            GameManager.Instance.GameOver();
    }
}
