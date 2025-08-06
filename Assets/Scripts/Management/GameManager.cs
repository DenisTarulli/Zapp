using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float songTempo;
    public float songTempoDivider;
    public float scrollingSpeed;
    public float scrollingSpeedMultiplier;
    [SerializeField] private float songStartDelay;
    [SerializeField] private bool isInfinity;
    [SerializeField] private float songDuration;
    [SerializeField] private Slider progressSlider;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI maxScoreText;
    [SerializeField] private float scoreMultiplier;
    private float score;
    private float maxScore;

    public static GameManager Instance { get; private set; }
    public bool IsInfinity { get => isInfinity; }

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
        {
            progressSlider.maxValue = songDuration;
            progressSlider.minValue = 0f;
            progressSlider.value = 0f;
        }        

        scrollingSpeed = songTempo / songTempoDivider;

        StartCoroutine(StartSong());
    }

    public void Update()
    {
        if (isInfinity)
        {
            scrollingSpeed += Time.deltaTime * scrollingSpeedMultiplier;
            Score();
            return;
        }

        if (progressSlider.value != progressSlider.maxValue)
            progressSlider.value += Time.deltaTime;        
    }

    private void Score()
    {
        score += Time.deltaTime * scoreMultiplier * (scrollingSpeed / 20f);
        scoreText.text = ((int)score).ToString("D6");
    }

    private void UpdateMaxScore()
    {
        maxScore = PlayerPrefs.GetFloat("MaxScore", 0f);

        if (score > maxScore)
        {
            PlayerPrefs.SetFloat("MaxScore", score);        
            maxScore = score;
        }

        maxScoreText.text = ((int)maxScore).ToString("D6");
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

        if (isInfinity)
        {
            UpdateCoins();
            UpdateMaxScore();
        }

        AudioManager.Instance.StopPlaying("Song");
    }

    public void Win()
    {
        Time.timeScale = 0f;
        UpdateCoins();

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

    private void UpdateCoins()
    {
        int amountToAdd = FindObjectOfType<CoinCounter>().CoinAmount;

        int newAmount = PlayerPrefs.GetInt("Coins") + amountToAdd;

        PlayerPrefs.SetInt("Coins", newAmount);
    }
}
