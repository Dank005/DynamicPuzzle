using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public static bool X2Coins = false;

    [SerializeField] private string AndroidAdID = "Interstitial_Android";
    [SerializeField] private string IosAdID = "Interstitial_iOS";

    private string AdID;

    private void Awake()
    {
        AdID = (Application.platform == RuntimePlatform.IPhonePlayer) ? IosAdID : AndroidAdID;
        LoadAd();
    }

    public void LoadAd()
    {
        Debug.Log("Loading Ad: " + AdID);
        Advertisement.Load(AdID, this);
    }

    public void ShowAd()
    {
        Debug.Log("Showing Ad: " + AdID);
        Advertisement.Show(AdID, this);
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        X2Coins = true;
        
        LoadAd();
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        throw new System.NotImplementedException();
    }
}
