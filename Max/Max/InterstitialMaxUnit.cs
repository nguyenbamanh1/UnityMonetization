using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ManhPackage.Unit.Max
{
    [Serializable]
    public class InterstitialMaxUnit : InterstitialUnit, IMaxListener
    {
        public InterstitialMaxUnit(string _adUnit) : base(_adUnit)
        {
            MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += OnMaxAdDisplayClosed;
            MaxSdkCallbacks.Interstitial.OnAdRevenuePaidEvent += OnMaxAdPaid;
            MaxSdkCallbacks.Interstitial.OnAdClickedEvent += OnMaxAdClicked;
            MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += OnMaxAdLoaded;
            MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += OnMaxAdLoadFaild;
            MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent += OnMaxAdDisplayFailed;
            MaxSdkCallbacks.Interstitial.OnAdDisplayedEvent += OnMaxAdDisplayed;
        }

        public override bool CanShowInterstitial()
        {
            return MaxSdk.IsInterstitialReady(_adUnit);
        }

        public override void LoadAd()
        {
            if (!MaxSdk.IsInterstitialReady(_adUnit))
            {
                Debug.LogError("Begin Load Inter");
                status = AdStatus.Loading;
                MaxSdk.LoadInterstitial(_adUnit);
            }
        }

        public override void ShowAd()
        {
            Debug.Log("Call Show Inter");
            if (MaxSdk.IsInterstitialReady(_adUnit))
                MaxSdk.ShowInterstitial(_adUnit);
            else
            {
                Debug.Log("But Inter Not Load");
                OnAdDisplayFaild(string.Empty);
            }
        }


        public void OnMaxAdLoaded(string adUnit, MaxSdkBase.AdInfo adInfo) => OnAdLoaded();

        public void OnMaxAdLoadFaild(string adUnit, MaxSdkBase.ErrorInfo adInfo) => OnAdLoadFaild(adInfo.ToString());

        public void OnMaxAdClicked(string adUnit, MaxSdkBase.AdInfo adInfo) => OnAdClicked();

        public void OnMaxAdPaid(string adUnit, MaxSdkBase.AdInfo adInfo) => OnAdPaid(new PaidFormat(adUnit, adInfo.Revenue));

        public void OnMaxAdDisplayed(string adUnit, MaxSdkBase.AdInfo adInfo) => OnAdDisplayOpened();

        public void OnMaxAdDisplayClosed(string adUnit, MaxSdkBase.AdInfo adInfo) => OnAdDisplayClosed();

        public void OnMaxAdDisplayFailed(string adUnit, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo) =>
            OnAdDisplayFaild(errorInfo.ToString());

        public void OnAdHiddenEvent(string adUnit, MaxSdkBase.AdInfo adInfo) => OnAdDisplayClosed();
    }
}
