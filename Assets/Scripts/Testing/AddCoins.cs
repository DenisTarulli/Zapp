using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class AddCoins : MonoBehaviour
{
    private void Awake()
    {
        PlayerPrefs.SetInt("Coins", 47);
    }
}
