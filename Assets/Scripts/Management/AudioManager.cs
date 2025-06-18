using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(1)]
public class AudioManager : Singleton<AudioManager>
{
    public Sound[] sounds;

    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
        }
    }

    public void StopPlaying(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning($"Sound: {sound} not found!");
            return;
        }

        s.source.Stop();
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning($"Sound: {name} was not found!");
        }

        s.source.Play();
    }

    private void Start()
    {
        foreach (Sound s in sounds)
        {
            if (s.playOnAwake)
                Play(s.name);
        }
    }

    public void SetPitch(string soundName, float newPitch)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);

        if (s == null)
        {
            Debug.LogWarning($"Sound: {soundName} was not found!");
        }

        s.source.pitch = newPitch;
    }

    public void PlayFaded(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning($"Sound: {name} was not found!");
        }

        StartCoroutine(FadeIn(name));
    }

    public IEnumerator FadeIn(string soundName)
    {
        float t = 0f;

        Sound s = Array.Find(sounds, sound => sound.name == soundName);

        if (s == null)
        {
            Debug.LogWarning($"Sound: {soundName} was not found!");
        }

        float maxVolume = s.volume;
        s.source.volume = 0f;
        s.source.Play();

        while (s.source.volume < maxVolume)
        {
            t += Time.deltaTime;
            float newVolume = t * maxVolume;
            s.source.volume = newVolume;
            yield return 0;
        }

        s.source.volume = maxVolume;
    }
}