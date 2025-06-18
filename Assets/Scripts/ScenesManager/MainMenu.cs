using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private SceneFader sceneFader;

    public void PlayGame()
    {
        sceneFader.FadeTo("MainScene");
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }
}
