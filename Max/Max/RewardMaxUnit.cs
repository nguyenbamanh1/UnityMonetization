using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManhPackage.Unit.Max
{
    [Serializable]
    public class RewardMaxUnit : RewardUnit
    {
        Action _rewardAction;

        public RewardMaxUnit(string _adUnit) : base(_adUnit)
        {
            MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += (s, a) => OnAdLoaded();
            MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += (s, a) => OnAdLoadFaild(a.ToString());
            MaxSdkCallbacks.Rewarded.OnAdDisplayedEvent += (s, a) => OnAdDisplayOpened();
            MaxSdkCallbacks.Rewarded.OnAdClickedEvent += (s, a) => OnAdClicked();
            MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += (s, a) =>
            {
                this._rewardAction?.Invoke();
                OnAdDisplayClosed();
            };
            MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += (s, e, a) => OnAdDisplayFaild(e.ToString());
            MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent += (s, a) => OnAdPaid(new PaidFormat(_adUnit, a.Revenue));
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
    }
}
