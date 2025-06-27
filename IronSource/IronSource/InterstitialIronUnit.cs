using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Services.LevelPlay;

namespace ManhPackage.Unit.IronSource
{
    public class InterstitialIronUnit : InterstitialUnit
    {
        LevelPlayInterstitialAd _interstitial;

        public InterstitialIronUnit(string _adUnit) : base(_adUnit)
        {
        }

        public override bool CanShowInterstitial()
        {
            return _interstitial != null && _interstitial.IsAdReady();
        }

        public override void LoadAd()
        {
            if (status == AdStatus.Loading) return;
            status = AdStatus.Loading;

            _interstitial = new LevelPlayInterstitialAd(_adUnit);
            _interstitial.OnAdLoaded += (i) => this.OnAdLoaded();
            _interstitial.OnAdLoadFailed += (e) => this.OnAdLoadFaild(e.ToString());
            _interstitial.OnAdDisplayed += (i) => this.OnAdDisplayOpened();
            _interstitial.OnAdDisplayFailed += (e) => this.OnAdDisplayFaild(e.ToString());
            _interstitial.OnAdClicked += (i) => this.OnAdClicked();
            _interstitial.OnAdClosed += (i) => this.OnAdDisplayClosed();
            _interstitial.LoadAd();
        }

        public override void ShowAd()
        {
            if (_interstitial != null && _interstitial.IsAdReady())
                _interstitial.ShowAd();
        }
    }
}
