using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string AnroidGameID = "4767539";
    [SerializeField] string IOSGameID = "4767538";
    [SerializeField] bool TestMode = true;

    private string GameID;

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads Initialization Complete");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed : {error} - message : {message}");
    }

    private void Awake()
    {
        Debug.Log($"Game platform is : {Application.platform}");
        GameID = (Application.platform == RuntimePlatform.IPhonePlayer ? IOSGameID : AnroidGameID);
        Advertisement.Initialize(GameID, TestMode, this);
    }
}
