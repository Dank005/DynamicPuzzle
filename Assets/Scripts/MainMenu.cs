using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GooglePlayGames;

public class MainMenu : MonoBehaviour
{
    public Coins coinsPanel;

    private void Start()
    {
        coinsPanel.UpdateCoins(0);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate(success =>
        {
            if (success)
            {
                //success authorization
            }
            else
            {

            }
        });
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowLeaderBoard()
    {
        Social.ShowLeaderboardUI();
    }
}