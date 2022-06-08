using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    public GameController gameController;
    public Button nextLevel;
    public Button bonuses;
    public Coins coinsPanel;
    public static bool isOn;

    public Text coinsText;
    public Text levelWin;

    private double coins;

    [HideInInspector] private const string leaderBoard = "CgkI26mvkdMIEAIQAQ";

    void UpdateRating(int numberLevel)
    {
        Social.ReportScore(numberLevel, leaderBoard, (bool success) => { });
    }

    public void SetActivePopup(double coins)
    {
        this.coins = coins;
        gameObject.SetActive(true);
        isOn = true;
        levelWin.text = $"Уровень {GameController.currentLevel+1} пройден";

        coinsText.text = this.coins.ToString();
    }

    public void NextLevel()
    {
        GameController.currentLevel += 1;
        UpdateRating(GameController.currentLevel); // rating
        PlayerPrefs.SetInt("level", GameController.currentLevel);

        gameController.SetCurrentLevel();      
        isOn = false;
        nextLevel.gameObject.SetActive(false);
        bonuses.gameObject.SetActive(true);
    }

    public void GetMoreCoins()
    {
        coins = coins * 2;
        InterstitialAds.X2Coins = false;
        JustGetCoins();
    }

    public void JustGetCoins()
    {
        coinsPanel.UpdateCoins((float)coins);
        nextLevel.gameObject.SetActive(true);
        bonuses.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
