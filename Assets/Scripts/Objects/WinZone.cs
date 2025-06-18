using UnityEngine;

public class WinZone : MonoBehaviour
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
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.Win();
        }
    }
}
