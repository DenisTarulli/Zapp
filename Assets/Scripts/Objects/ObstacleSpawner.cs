using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private Transform spawnPointsContainer;
    [SerializeField] private float obstacleSpawnInterval;
    [SerializeField] private int biasedWeight;
    [SerializeField, Range(0, 3)] private int minimumObstacles;
    private Transform[] spawnPoints = new Transform[6];

    private void Awake()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPoints[i] = spawnPointsContainer.GetChild(i).transform;
        }
    }

    private void Start()
    {
        InvokeRepeating(nameof(SpawnObstacles), 0f, obstacleSpawnInterval);
    }

    private void SpawnObstacles()
    {
        int[] weightedCounts = new int[] { 0, 1, 2, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5 };
        int randomAmount = weightedCounts[(Random.Range(0, weightedCounts.Length))];

        if (randomAmount < minimumObstacles)
            randomAmount = minimumObstacles;

        randomAmount = Mathf.Min(randomAmount, spawnPoints.Length);

        List<Transform> availableTargets = new(spawnPoints);

        for (int i = 0; i < randomAmount; i++)
        {
            int index = GetBiasedIndex(availableTargets.Count);

            Transform selectedSpawnPoint = availableTargets[index];

            Instantiate(obstaclePrefab, selectedSpawnPoint.position, Quaternion.identity);

            availableTargets.RemoveAt(index);
        }
    }

    private int GetBiasedIndex(int listCount)
    {
        List<int> weightedIndices = new();

        for (int i = 0; i < listCount; i++)
        {
            int weight;

            if ((i >= 3))            
                weight = biasedWeight;            
            else            
                weight = 1;            

            for (int j = 0; j < weight; j++)
            {
                weightedIndices.Add(i);
            }
        }

        return weightedIndices[Random.Range(0, weightedIndices.Count)];
    }
}
