using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Bonuses : MonoBehaviour
{
    public GameObject BonusesPopup;
    public Coins coinsPanel;
    public Button ReduceSpeedButton;
    public Button NativeRotationdButton;
    public Popup popup;

    public void OpenBonusesPopup()
    {
        BonusesPopup.SetActive(true);
    }

    public void CloseBonusesPopup()
    {
        BonusesPopup.SetActive(false);
    }

    public void ReduceSpeed()
    {
        if (Coins.GetCoinsNumber() > 350 && ReduceSpeedButton.interactable)
        {
            ReduceSpeedButton.interactable = false;
            var levelPrefab = GameObject.FindGameObjectWithTag("LevelPrefab");
            var video = levelPrefab.GetComponent<VideoPlayer>();
            video.playbackSpeed = video.playbackSpeed * 0.5f;

            coinsPanel.UpdateCoins(-350f);
        }
    }

    public void NativeRotaion()
    {
        if (Coins.GetCoinsNumber() > 250 && NativeRotationdButton.interactable)
        {
            NativeRotationdButton.interactable = false;
            var playfieldTiles = GameObject.FindGameObjectWithTag("Playfield").GetComponent<Playfield>().tiles;
            foreach (var tile in playfieldTiles)
            {
                tile.transform.rotation = Quaternion.identity;
            }
            coinsPanel.UpdateCoins(-250f);
        }            
    }
}
