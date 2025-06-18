using System.Collections;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private AudioSource src;
    private float maxVolume;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        src = objs[0].GetComponent<AudioSource>();
        maxVolume = src.volume;        
    }

    public void StartMusic()
    {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float t = 0f;
        src.volume = 0f;
        src.Play();

        while (src.volume < maxVolume)
        {
            t += Time.deltaTime;
            float newVolume = t * maxVolume;
            src.volume = newVolume;
            yield return 0;
        }

        src.volume = maxVolume;
    }
}
