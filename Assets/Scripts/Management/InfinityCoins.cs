using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinityCoins : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private Transform spawnPointsContainer;
    [SerializeField] private float spawnDelay;
    private Transform[] spawnPoints;

    private void Start()
    {
        spawnPoints = new Transform[spawnPointsContainer.childCount];

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPoints[i] = spawnPointsContainer.GetChild(i).transform;
        }

        InvokeRepeating(nameof(SpawnCoin), 0f, spawnDelay);
    }

    private void SpawnCoin()
    {
        int randomSpawn = Random.Range(0, spawnPoints.Length);

        Instantiate(coinPrefab, spawnPoints[randomSpawn].position, Quaternion.identity);
    }
}
