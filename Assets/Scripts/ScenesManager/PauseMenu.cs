using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;

    private bool isPaused = false;
    private TouchControls inputActions;

    private void Awake()
    {
        inputActions = new TouchControls();
        inputActions.Keyboard.Enable();
        inputActions.Keyboard.Pause.performed += TogglePauseKeyboard;
    }

    private void OnDestroy()
    {
        inputActions.Keyboard.Disable();
    }

    void Start()
    {
        pausePanel.SetActive(false);
    }

    private void TogglePauseKeyboard(InputAction.CallbackContext context)
    {
        TogglePause();
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            AudioManager.Instance.Pause("Song");
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            pausePanel.SetActive(false);
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        AudioManager.Instance.Unpause("Song");
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
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