using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public GameObject SettingsPopup;
    public Coins coinsPanel;

    public void ResetProgress()
    {
        PlayerPrefs.SetInt("level", 0);
        var allCoins = PlayerPrefs.GetFloat("coins");
        coinsPanel.UpdateCoins(-allCoins);

    }
}
