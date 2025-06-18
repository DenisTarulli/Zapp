using System.Collections;
using UnityEngine;

public class ModeSelector : MonoBehaviour
{
    [SerializeField] private SceneFader sceneFader;

    public void InfinityMode()
    {
        sceneFader.FadeTo("InfinityLevel");
        StartCoroutine(FadeOutMusic());
    }

    public void LevelsMode()
    {
        sceneFader.FadeTo("LevelSelector");
    }

    private IEnumerator FadeOutMusic()
    {
        float t = 1f;

        if (!GameObject.FindGameObjectWithTag("Music").TryGetComponent<AudioSource>(out var src)) yield break;

        float maxVolume = src.volume;

        while (t > 0f)
        {
            t -= Time.deltaTime;
            float newVolume = t * maxVolume;
            src.volume = newVolume;
            yield return 0;
        }

        src.Stop();
    }
}
