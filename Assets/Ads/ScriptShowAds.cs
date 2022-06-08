using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptShowAds : MonoBehaviour
{
    public InterstitialAds ad;
    public RewardedAds rewAd;

    public void ShowAd()
    {
        ad.ShowAd();
    }

    public void ShowRewAd()
    {
        rewAd.ShowAd();
    }
}
