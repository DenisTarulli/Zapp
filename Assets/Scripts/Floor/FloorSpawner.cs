using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpawner : MonoBehaviour
{
    [SerializeField] private FloorTile[] groundPrefabs;
    [SerializeField] private FloorTile emptyFloorTilePrefab;
    [SerializeField] private int initialEmptyTiles = 2;
    [SerializeField] private Vector3 nextSpawnPoint;

    private int tilesSpawned = 0;

    public void SpawnTile()
    {
        FloorTile selectedPrefab;

        if (tilesSpawned < initialEmptyTiles)
        {
            selectedPrefab = emptyFloorTilePrefab;
        }
        else
        {
            int randomIndex = Random.Range(0, groundPrefabs.Length);
            selectedPrefab = groundPrefabs[randomIndex];
        }

        nextSpawnPoint = Instantiate(selectedPrefab, nextSpawnPoint, Quaternion.identity).Setup(this)
            .transform.GetChild(1).transform.position;

        tilesSpawned++;
    }

    void Start()
    {
        for (int i = 0; i < 7; i++)
        {
            SpawnTile();
        }
    }
}
