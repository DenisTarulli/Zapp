using UnityEngine;

public class FloorTile : MonoBehaviour
{
    [SerializeField] private float destroyDelay;
    private FloorSpawner floorSpawner;

    private void Update()
    {
        transform.position -= new Vector3(0f, 0f, GameManager.Instance.scrollingSpeed * Time.deltaTime);
    }

    public FloorTile Setup(FloorSpawner groundSpawner)
    {
        this.floorSpawner = groundSpawner;
        return this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Despawner"))
        {
            Destroy(gameObject, destroyDelay);
        }


        if (other.gameObject.CompareTag("Spawner"))
        {
            floorSpawner.SpawnTile();
        }
    }
}
