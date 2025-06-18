using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static event Action OnCoinCollected;
    private float scrollSpeed;

    private void Start()
    {
        scrollSpeed = GameManager.Instance.songTempo / GameManager.Instance.songTempoDivider;
    }

    private void Update()
    {
        transform.position -= new Vector3(0f, 0f, scrollSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        OnCoinCollected?.Invoke();
        AudioManager.Instance.Play("Coin");
        Destroy(gameObject);
    }
}
