using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float songTempo;
    public float songTempoDivider;
    [SerializeField] private float songStartDelay;
    [SerializeField] private bool isInfinity;

    public static GameManager Instance { get; private set; }

    [Header("UI Panels")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;

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

    private void Start()
    {
        if (!isInfinity)
            StartCoroutine(StartSong());
    }

    public IEnumerator StartSong()
    {
        yield return new WaitForSeconds(songStartDelay);
        AudioManager.Instance.PlayFaded("Song");
    }

    public void GameOver()
    {
        Time.timeScale = 0f;

        if (losePanel != null)
            losePanel.SetActive(true);

        AudioManager.Instance.StopPlaying("Song");
    }

    public void Win()
    {
        Time.timeScale = 0f;

        if (winPanel != null)
            winPanel.SetActive(true);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
