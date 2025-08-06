using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Movimiento")]
    public bool moves = true; // Esto aparece en el inspector

    private void Update()
    {
        if (!moves) return; // Si no se mueve, salimos
        transform.position -= new Vector3(0f, 0f, GameManager.Instance.scrollingSpeed * Time.deltaTime);
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
