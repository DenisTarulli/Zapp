using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
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
        if (other.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            playerHealth.TakeDamage();
            Destroy(gameObject);
        }

        if (!other.gameObject.CompareTag("Despawner")) return;
        Destroy(gameObject);
    }
}
