using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField, Range(0.2f, 3f)] private float invulnerabilityTime;
    private GameObject invulnerabilityEffect;
    private bool canTakeDamage;
    private int currentHealth;

    [SerializeField] private Transform heartsContainer;
    private GameObject[] hearts;

    public bool CanTakeDamage { get => canTakeDamage; set => canTakeDamage = value; }
    public bool PowerUpActive;

    public static event Action OnPlayerDied;
    public static event Action OnPlayerDamaged;

    private void Start()
    {
        hearts = new GameObject[heartsContainer.childCount];

        for (int i = 0; i < heartsContainer.childCount; i++)
        {
            hearts[i] = heartsContainer.GetChild(i).gameObject;
        }

        invulnerabilityEffect = transform.GetChild(0).gameObject;
        canTakeDamage = true;
        currentHealth = maxHealth;
    }

    public void TakeDamage()
    {
        if (!canTakeDamage) return;

        StartCoroutine(InvulnerabilityFrames());

        currentHealth--;

        for (int i = 0; i < hearts.Length; i++)
        {
            if ((currentHealth - 1) < i)
                hearts[i].SetActive(false);
        }

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