using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsText;
    private int coinAmount;

    private void Start()
    {
        coinAmount = 0;
        coinsText.text = $"Coins: {coinAmount}";
    }

    private void AddCoin()
    {
        coinAmount++;
        coinsText.text = $"Coins: {coinAmount}";
    }

    private void OnEnable()
    {
        Coin.OnCoinCollected += AddCoin;
    }

    private void OnDisable()
    {
        Coin.OnCoinCollected -= AddCoin;        
    }
}
