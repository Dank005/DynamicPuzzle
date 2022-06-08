using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class RewardedAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private Button buttonShowRewAd;

    [SerializeField] private string AndroidAdID = "Rewarded_Android";
    [SerializeField] private string IosAdID = "Rewarded_iOS";

    private string AdID;

    private void Awake()
    {
        AdID = (Application.platform == RuntimePlatform.IPhonePlayer) ? IosAdID : AndroidAdID;
        LoadAd();
    }

    private void Start()
    {
        Debug.Log("Loading Ad: " + AdID);
        Advertisement.Load(AdID, this);
    }

    public void LoadAd()
    {
        Debug.Log("Loading Ad: " + AdID);
        Advertisement.Load(AdID, this);
    }

    public void ShowAd()
    {
        buttonShowRewAd.interactable = false;
        Debug.Log("Showing RewAd: " + AdID);
        Advertisement.Show(AdID, this);
    }
    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Ad Loaded" + placementId);

        if (AdID.Equals(placementId))
        {
            buttonShowRewAd.onClick.AddListener(ShowAd);
            buttonShowRewAd.interactable = true;
        }
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        throw new System.NotImplementedException();
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
        if (AdID.Equals (placementId) && showCompletionState.Equals(UnityAdsCompletionState.COMPLETED))
        {
            Debug.Log("Reward");
        }    
    }
}
