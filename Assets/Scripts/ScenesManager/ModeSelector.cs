using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSelector : MonoBehaviour
{
    [SerializeField] private SceneFader sceneFader;

    public void InfinityMode()
    {
        sceneFader.FadeTo("InfinityLevel");
    }

    public void LevelsMode()
    {
        sceneFader.FadeTo("LevelSelector");
    }
}
