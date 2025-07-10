using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinButton : MonoBehaviour
{
    [SerializeField] private string skinName;
    [SerializeField] private Material skinMaterial;
    [SerializeField] private int skinPrice;
    private GameObject setSkinButton;
    private Button skinButton;
    private bool skinAcquired;

    public string SkinName { get => skinName; }
    public int SkinPrice { get => skinPrice; }
    public GameObject SetSkinButton { get => setSkinButton; }
    public bool SkinAcquired { get => skinAcquired; set => skinAcquired = value; }

    private void Awake()
    {
        setSkinButton = transform.GetChild(0).gameObject;
        int prefValue = PlayerPrefs.GetInt(skinName, 0);

        if (prefValue == 1)
            skinAcquired = true;
        else
            skinAcquired = false;

        skinButton = GetComponent<Button>();

        if (skinAcquired)
        {
            skinButton.interactable = false;
            SetSkinButtonActive();
        }
        else
        {
            ActivateInteraction();
        }
    }

    public void ActivateInteraction()
    {
        if (PlayerPrefs.GetInt("Coins") >= skinPrice && !skinAcquired)
            skinButton.interactable = true;
        else
            skinButton.interactable = false;
    }

    public void SetSkinButtonActive()
    {
        setSkinButton.SetActive(true);
    }

    public void SetButtonInteraction(bool newState)
    {
        setSkinButton.GetComponent<Button>().interactable = newState;
    }
}
