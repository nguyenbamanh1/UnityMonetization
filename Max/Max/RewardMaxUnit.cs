using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManhPackage.Unit.Max
{
    [Serializable]
    public class RewardMaxUnit : RewardUnit, IMaxListener
    {
        Action _rewardAction;

        public RewardMaxUnit(string _adUnit) : base(_adUnit)
        {
            MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += OnMaxAdDisplayClosed;
            MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent += OnMaxAdPaid;
            MaxSdkCallbacks.Rewarded.OnAdClickedEvent += OnMaxAdClicked;
            MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += OnMaxAdLoaded;
            MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += OnMaxAdLoadFaild;
            MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += OnMaxAdDisplayFailed;
            MaxSdkCallbacks.Rewarded.OnAdDisplayedEvent += OnMaxAdDisplayed;
            MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += (adUnit, rewarded ,adInfo) =>
            {
                this._rewardAction?.Invoke();
            };
        }

        public override void LoadAd()
        {

            if (!MaxSdk.IsRewardedAdReady(_adUnit))
                MaxSdk.LoadRewardedAd(_adUnit);
        }

        public override void ShowAd(Action rewardAction)
        {
            if (MaxSdk.IsRewardedAdReady(_adUnit))
                MaxSdk.ShowRewardedAd(_adUnit);
            else
                LoadAd();
        }

        public void OnMaxAdLoaded(string adUnit, MaxSdkBase.AdInfo adInfo) => OnAdLoaded();

        public void OnMaxAdLoadFaild(string adUnit, MaxSdkBase.ErrorInfo adInfo) => OnAdLoadFaild(adInfo.ToString());

        public void OnMaxAdClicked(string adUnit, MaxSdkBase.AdInfo adInfo) => OnAdClicked();

        public void OnMaxAdPaid(string adUnit, MaxSdkBase.AdInfo adInfo) => OnAdPaid(new PaidFormat(adUnit, adInfo.Revenue));

        public void OnMaxAdDisplayed(string adUnit, MaxSdkBase.AdInfo adInfo) => OnAdDisplayOpened();

        public void OnMaxAdDisplayClosed(string adUnit, MaxSdkBase.AdInfo adInfo)
        {
            OnAdDisplayClosed();
        }

        public void OnMaxAdDisplayFailed(string adUnit, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo) =>
            OnAdDisplayFaild(errorInfo.ToString());

        public void OnAdHiddenEvent(string adUnit, MaxSdkBase.AdInfo adInfo) => OnAdDisplayClosed();
    }
}
