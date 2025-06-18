using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private int maxHealth;
    [SerializeField, Range(0.2f, 1f)] private float invulnerabilityTime;
    private bool canTakeDamage;
    private int currentHealth;

    public static event Action OnPlayerDied;
    public static event Action OnPlayerDamaged;

    private void Start()
    {
        canTakeDamage = true;
        currentHealth = maxHealth;
        healthText.text = $"HP: {currentHealth}";
    }

    public void TakeDamage()
    {
        if (!canTakeDamage) return;

        StartCoroutine(InvulnerabilityFrames());

        currentHealth--;
        healthText.text = $"HP: {currentHealth}";

        OnPlayerDamaged?.Invoke();

        if (currentHealth <= 0)
        {
            OnPlayerDied?.Invoke();
            GameManager.Instance.GameOver();
        }
    }

    private IEnumerator InvulnerabilityFrames()
    {
        canTakeDamage = false;

        yield return new WaitForSeconds(invulnerabilityTime);

        canTakeDamage = true;
    }
}
