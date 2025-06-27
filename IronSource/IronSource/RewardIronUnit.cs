#define IRONSOURCE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Services.LevelPlay;
namespace ManhPackage.Unit.IronSource
{
    public class RewardIronUnit : RewardUnit
    {
        LevelPlayRewardedAd _reward;
        Action _actionReward;
        public RewardIronUnit(string _adUnit) : base(_adUnit)
        {
        }

        public override void LoadAd()
        {
            if (status == AdStatus.Loading) return;
            status = AdStatus.Loading;

            _reward = new LevelPlayRewardedAd(_adUnit);
            _reward.OnAdLoaded += (i) => this.OnAdLoaded();
            _reward.OnAdLoadFailed += (e) => this.OnAdLoadFaild(e.ToString());
            _reward.OnAdDisplayed += (i) => this.OnAdDisplayOpened();
            _reward.OnAdDisplayFailed += (e) => this.OnAdDisplayFaild(e.ToString());
            _reward.OnAdClicked += (i) => this.OnAdClicked();
            _reward.OnAdRewarded += (i, a) => IronSourceEventsDispatcher.executeAction(_actionReward);
            _reward.OnAdClosed += (i) => this.OnAdDisplayClosed();
            _reward.LoadAd();
        }

        public override void ShowAd(Action rewardAction)
        {
            if (_reward != null && _reward.IsAdReady())
            {
                _actionReward = rewardAction;
                _reward.ShowAd();
            }
        }
    }
}
