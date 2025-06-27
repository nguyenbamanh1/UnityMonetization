using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManhPackage.Unit.Admob
{
    [Serializable]
    public class RewardAdmobUnit : RewardUnit
    {
        RewardedAd _rewardedAd;

        public RewardAdmobUnit(string _adUnit) : base(_adUnit)
        {
        }

        public override void LoadAd()
        {
            if (status == AdStatus.Loading)
                return;
            status = AdStatus.Loading;
            RewardedAd.Load(_adUnit, new AdRequest(), (ad, err) =>
            {
                if (err != null)
                {
                    OnAdLoadFaild(err.ToString());
                    return;
                }
                _rewardedAd = ad;
                _rewardedAd.OnAdPaid += (a) => OnAdPaid(new PaidFormat(_adUnit, a.CurrencyCode, a.Value));
                _rewardedAd.OnAdClicked += OnAdClicked;
                _rewardedAd.OnAdFullScreenContentClosed += OnAdDisplayClosed;
                _rewardedAd.OnAdFullScreenContentFailed += (e) => OnAdDisplayFaild(e.ToString());
                _rewardedAd.OnAdFullScreenContentOpened += OnAdDisplayOpened;
                OnAdLoaded();
            });
        }

        public override void ShowAd(Action rewardAction)
        {
            if (_rewardedAd != null && _rewardedAd.CanShowAd())
            {
                _rewardedAd.Show((r) =>
                {
                    MobileAdsEventExecutor.ExecuteInUpdate(rewardAction);
                });
            }
            else
            {
                LoadAd();
            }
        }
    }
}
