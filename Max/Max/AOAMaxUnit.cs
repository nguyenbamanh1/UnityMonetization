using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityMonetization.Unit.Max
{
    [Serializable]
    public class AOAMaxUnit : AppOpenUnit, IMaxListener
    {
        public AOAMaxUnit(string _adUnit) : base(_adUnit)
        {
            MaxSdkCallbacks.AppOpen.OnAdHiddenEvent += OnMaxAdDisplayClosed;
            MaxSdkCallbacks.AppOpen.OnAdRevenuePaidEvent += OnMaxAdPaid;
            MaxSdkCallbacks.AppOpen.OnAdClickedEvent += OnMaxAdClicked;
            MaxSdkCallbacks.AppOpen.OnAdLoadedEvent += OnMaxAdLoaded;
            MaxSdkCallbacks.AppOpen.OnAdLoadFailedEvent += OnMaxAdLoadFaild;
            MaxSdkCallbacks.AppOpen.OnAdDisplayFailedEvent += OnMaxAdDisplayFailed;
            MaxSdkCallbacks.AppOpen.OnAdDisplayedEvent += OnMaxAdDisplayed;
        }

        public override bool CanShow()
        {
            return MaxSdk.IsAppOpenAdReady(_adUnit);
        }

        public override void LoadAd()
        {
            if (!MaxSdk.IsAppOpenAdReady(_adUnit))
            {
                status = AdStatus.Loading;
                MaxSdk.LoadAppOpenAd(_adUnit);
            }
        }

        public override void ShowAd()
        {
            if (MaxSdk.IsAppOpenAdReady(_adUnit))
                MaxSdk.ShowAppOpenAd(_adUnit);
            else
                LoadAd();
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
