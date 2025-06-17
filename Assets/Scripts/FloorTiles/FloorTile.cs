using UnityEngine;

public class FloorTile : MonoBehaviour
{
    [SerializeField] private float destroyDelay;
    private FloorSpawner floorSpawner;
    private float scrollSpeed;

    private void Start()
    {
        scrollSpeed = floorSpawner.beatTempo / 30f;
    }

    private void Update()
    {
        transform.position -= new Vector3(0f, 0f, scrollSpeed * Time.deltaTime);
    }

    public FloorTile Setup(FloorSpawner groundSpawner)
    {
        this.floorSpawner = groundSpawner;
        return this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Despawner")) return;

        floorSpawner.SpawnTile();
        Destroy(gameObject, destroyDelay);
    }
}
