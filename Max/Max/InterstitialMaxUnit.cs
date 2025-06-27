using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ManhPackage.Unit.Max
{
    [Serializable]
    public class InterstitialMaxUnit : InterstitialUnit
    {
        public InterstitialMaxUnit(string _adUnit) : base(_adUnit)
        {
            MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += (s, a) => OnAdLoaded();
            MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += (s, a) => OnAdLoadFaild(a.ToString());
            MaxSdkCallbacks.Interstitial.OnAdDisplayedEvent += (s, a) => OnAdDisplayOpened();
            MaxSdkCallbacks.Interstitial.OnAdClickedEvent += (s, a) => OnAdClicked();
            MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += (s, a) => OnAdDisplayClosed();
            MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent += (s, e, a) => OnAdDisplayFaild(e.ToString());
            MaxSdkCallbacks.Interstitial.OnAdRevenuePaidEvent += (s, a) => OnAdPaid(new PaidFormat(_adUnit, a.Revenue));
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
    }
}
