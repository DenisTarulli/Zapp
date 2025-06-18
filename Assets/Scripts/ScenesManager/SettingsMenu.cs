using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    private void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("Volume", 1f);
        volumeSlider.value = savedVolume;
    }
}
