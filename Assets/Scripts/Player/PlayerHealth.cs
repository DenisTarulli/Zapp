using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private int maxHealth;
    private int currentHealth;

    public static event Action OnPlayerDied;
    public static event Action OnPlayerDamaged;

    private void Start()
    {
        currentHealth = maxHealth;
        healthText.text = $"HP: {currentHealth}";
    }

    public void TakeDamage()
    {
        currentHealth--;
        healthText.text = $"HP: {currentHealth}";

        OnPlayerDamaged?.Invoke();

        if (currentHealth <= 0)
        {
            OnPlayerDied?.Invoke();
            GameManager.Instance.GameOver();
        }
    }
}
