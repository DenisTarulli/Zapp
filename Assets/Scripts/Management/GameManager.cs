using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float songTempo;
    public float songTempoDivider;
    [SerializeField] private float songStartDelay;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Start()
    {
        StartCoroutine(StartSong());
    }

    public IEnumerator StartSong()
    {
        yield return new WaitForSeconds(songStartDelay);

        AudioManager.Instance.Play("Song");
    }
}
