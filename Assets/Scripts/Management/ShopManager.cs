using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private SceneFader sceneFader;
    [SerializeField] private Transform buttonsContainter;
    [SerializeField] private TextMeshProUGUI coinsText;
    private SkinButton[] buttons;
    private SkinButton currentButton;

    private void Start()
    {
        int buttonsAmount = buttonsContainter.childCount;
        buttons = new SkinButton[buttonsAmount];

        for (int i = 0; i < buttonsAmount; i++)
        {
            buttons[i] = buttonsContainter.GetChild(i).GetComponent<SkinButton>();
        }

        SetSelectedButtons();

        SelectSkin(PlayerPrefs.GetInt("Skin", 0));

        SetCoins();
    }

    public void TryBuySkin(string name)
    {
        for (int i = 0;  i < buttons.Length; i++)
        {
            if (buttons[i].SkinName == name)
                currentButton = buttons[i];
        }

        if (currentButton == null) return;

        int coins = GetCoins();

        int price = currentButton.SkinPrice;

        if (coins >= price)
        {
            PlayerPrefs.SetInt(name, 1);
            currentButton.GetComponent<Button>().interactable = false;

            coins -= price;
            PlayerPrefs.SetInt("Coins", coins);

            currentButton.SetSkinButtonActive();
            currentButton.SkinAcquired = true;

            SetCoins();

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].ActivateInteraction();
            }
        }
    }

    private int GetCoins()
    {
        return PlayerPrefs.GetInt("Coins", 0);
    }

    public void SelectSkin(int skinIndex)
    {
        PlayerPrefs.SetInt("Skin", skinIndex);

        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].SetSkinButton.activeSelf)
                if (skinIndex != (i + 1))
                    buttons[i].SetButtonInteraction(true);
                else
                    buttons[i].SetButtonInteraction(false);
        }
    }

    private void SetSelectedButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].SetSkinButton.activeSelf)
                if (PlayerPrefs.GetInt("Skin") != (i + 1))
                    buttons[i].SetButtonInteraction(true);
                else
                    buttons[i].SetButtonInteraction(false);
        }
    }

    public void BackToMenu()
    {
        sceneFader.FadeTo("MainMenu");
    }

    private void SetCoins()
    {
        coinsText.text = $"COINS: {PlayerPrefs.GetInt("Coins", 0)}";
    }
}
