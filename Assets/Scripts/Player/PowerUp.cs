using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float maxCooldown;
    [SerializeField] private GameObject invulnerabilityEffect;
    [SerializeField] private float duration;
    [SerializeField] private TextMeshProUGUI cooldownText;
    [SerializeField] private Button powerUpButton;
    private float currentCooldown;

    private PlayerHealth playerHealth;

    private bool powerUpActive;
    private bool powerUpReady;

    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        currentCooldown = maxCooldown;
    }

    private void Update()
    {
        if (currentCooldown <= 0)
        {
            currentCooldown = 0;
            powerUpReady = true;
            cooldownText.text = "";
        }
        else if (!powerUpActive)
        {
            cooldownText.text = Mathf.Ceil(currentCooldown).ToString();
        }

        if (!powerUpReady && !powerUpActive)
        {
            currentCooldown -= Time.deltaTime;
            powerUpButton.interactable = false;
        }
        else if (powerUpReady && !powerUpActive)
        {
            powerUpButton.interactable = true;
        }
        else
        {
            powerUpButton.interactable = false;
        }
    }

    public void ActivatePowerUp()
    {
        StartCoroutine(InvulnerabilityPowerUp());
    }

    private IEnumerator InvulnerabilityPowerUp()
    {
        if (powerUpActive || !powerUpReady) yield break;

        powerUpActive = true;
        powerUpReady = false;
        playerHealth.CanTakeDamage = false;
        invulnerabilityEffect.SetActive(true);

        yield return new WaitForSeconds(duration);

        powerUpActive = false;
        playerHealth.CanTakeDamage = true;
        invulnerabilityEffect.SetActive(false);
        currentCooldown = maxCooldown;
    }
}
