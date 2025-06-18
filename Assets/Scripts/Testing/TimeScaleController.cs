using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleController : MonoBehaviour
{
    [SerializeField, Range(1.5f, 3f)] private float fastTimeScale;

    public void IncreaseTimeScale()
    {
        Time.timeScale = fastTimeScale;
        AudioManager.Instance.SetPitch("Song", fastTimeScale);
    }

    public void DefaultTimeScale()
    {
        Time.timeScale = 1f;
        AudioManager.Instance.SetPitch("Song", 1f);
    }
}
