using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTile : MonoBehaviour
{
    private FloorSpawner groundSpawner;

    public FloorTile Setup(FloorSpawner groundSpawner)
    {
        this.groundSpawner = groundSpawner;
        return this;
    }

    private void OnTriggerExit(Collider other)

    {
        groundSpawner.SpawnTile();
        Destroy(gameObject, 2f);
    }
}
