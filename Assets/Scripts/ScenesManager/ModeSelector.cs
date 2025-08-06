using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ModeSelector : MonoBehaviour
{
    [SerializeField] private SceneFader sceneFader;

    public void InfinityMode()
    {
        StartCoroutine(FadeOutMusic());
        sceneFader.FadeTo("InfinityLevel");
    }

    public void LevelsMode()
    {
        sceneFader.FadeTo("LevelSelector");
    }

    public void Back(string previousSceneName)
    {
        sceneFader.FadeTo(previousSceneName);
    }

    private IEnumerator FadeOutMusic()
    {
        float t = 1f;

        if (!GameObject.FindGameObjectWithTag("Music").TryGetComponent<AudioSource>(out var src)) yield break;

        float maxVolume = src.volume;

        while (t > 0f)
        {
            t -= Time.deltaTime * sceneFader.animationSpeed;
            float newVolume = t * maxVolume;
            src.volume = newVolume;
            yield return 0;
        }

        src.Stop();
    }
}
