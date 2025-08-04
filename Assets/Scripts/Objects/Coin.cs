using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static event Action OnCoinCollected;

    private void Update()
    {
        transform.position -= new Vector3(0f, 0f, GameManager.Instance.scrollingSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        OnCoinCollected?.Invoke();
        AudioManager.Instance.Play("Coin");
        Destroy(gameObject);
    }
}
