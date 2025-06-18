using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private SceneFader sceneFader;

    public void SelectLevel(string level)
    {
        sceneFader.FadeTo(level);
        StartCoroutine(FadeOutMusic());
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
