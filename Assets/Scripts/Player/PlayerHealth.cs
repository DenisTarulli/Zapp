using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private int maxHealth;
    [SerializeField, Range(0.2f, 3f)] private float invulnerabilityTime;
    private GameObject invulnerabilityEffect;
    private bool canTakeDamage;
    private int currentHealth;

    public bool CanTakeDamage { get => canTakeDamage; set => canTakeDamage = value; }
    public bool PowerUpActive;

    public static event Action OnPlayerDied;
    public static event Action OnPlayerDamaged;

    private void Start()
    {
        invulnerabilityEffect = transform.GetChild(0).gameObject;
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

        AudioManager.Instance.Play("Hurt");
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
        invulnerabilityEffect.SetActive(true);

        yield return new WaitForSeconds(invulnerabilityTime);
        if (!PowerUpActive)
            canTakeDamage = true;

        invulnerabilityEffect.SetActive(false);
    }
}