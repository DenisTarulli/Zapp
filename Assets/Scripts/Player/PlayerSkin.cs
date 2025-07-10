using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    [SerializeField] private Material[] skins;
    [SerializeField] private int newSkin;
    private int currentSkinIndex;

    private void Start()
    {
        currentSkinIndex = PlayerPrefs.GetInt("Skin", 0);
        
        gameObject.GetComponent<Renderer>().material = skins[currentSkinIndex];
    }
}
