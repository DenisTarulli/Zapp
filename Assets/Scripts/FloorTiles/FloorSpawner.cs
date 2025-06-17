using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSpawner : MonoBehaviour
{
    [SerializeField] private FloorTile emptyFloorTilePrefab;
    [SerializeField] private int initialTiles = 2;
    [SerializeField] private Vector3 newSpawnPoint;
    [SerializeField] private Transform spawnPoint;

    public float beatTempo;

    public void SpawnInitialTile()
    {
        newSpawnPoint = Instantiate(emptyFloorTilePrefab, newSpawnPoint, Quaternion.identity).Setup(this)
            .transform.GetChild(1).transform.position;
    }

    public void SpawnTile()
    {
        Instantiate(emptyFloorTilePrefab, spawnPoint.position, Quaternion.identity).Setup(this);
    }

    void Start()
    {
        spawnPoint.position = new Vector3(0f, 0f, initialTiles * 15f);

        for (int i = 0; i < initialTiles; i++)
        {
            SpawnInitialTile();
        }

        SpawnTile();
    }
}
