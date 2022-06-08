using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coins : MonoBehaviour
{
    public Text coinsText;
    private float lastCoins;
    
    public void UpdateCoins(float coins)
    {
        lastCoins = coins;

        var coinsUp = PlayerPrefs.GetFloat("coins");
        var result = coinsUp + coins;
        coinsText.text = result.ToString();
        PlayerPrefs.SetFloat("coins", result);
    }

    public void X2Coins()
    {
        var coinsUp = PlayerPrefs.GetFloat("coins");
        var result = coinsUp + lastCoins;
        coinsText.text = result.ToString();
        PlayerPrefs.SetFloat("coins", result);
    }

    public static float GetCoinsNumber()
    {
        return PlayerPrefs.GetFloat("coins");
    }

    public void ReverseCoins()
    {
        var coinsUp = PlayerPrefs.GetFloat("coins");
        var result = coinsUp - lastCoins;
        coinsText.text = result.ToString();
        PlayerPrefs.SetFloat("coins", result);
    }
}
