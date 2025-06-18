using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("Volume"))
        {
            PlayerPrefs.SetFloat("Volume", 1);
        }
        else
            Load();
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        AudioListener.volume = volumeSlider.value;
    }
}
