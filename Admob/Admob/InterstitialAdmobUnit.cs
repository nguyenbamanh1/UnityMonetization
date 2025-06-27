using GoogleMobileAds.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManhPackage.Unit.Admob
{
    [Serializable]
    public class InterstitialAdmobUnit : InterstitialUnit
    {
        InterstitialAd _interstitial;
        public InterstitialAdmobUnit(string _adUnit) : base(_adUnit)
        {
        }

        public override bool CanShowInterstitial()
        {
            return _interstitial != null && _interstitial.CanShowAd() && status == AdStatus.Ready;
        }

        public override void LoadAd()
        {
            if (status == AdStatus.Loading)
                return;
            status = AdStatus.Loading;
            var adRequest = new AdRequest();
            adRequest.Keywords.Add("unity-admob-sample");

            InterstitialAd.Load(_adUnit, adRequest,
                (InterstitialAd ad, LoadAdError error) =>
                {
                    // if error is not null, the load request failed.
                    if (error != null || ad == null)
                    {
                        OnAdLoadFaild(error.ToString());
                        return;
                    }
                    _interstitial = ad;
                    _interstitial.OnAdPaid += (v) => OnAdPaid(new PaidFormat(_adUnit, v.CurrencyCode, v.Value));
                    _interstitial.OnAdClicked += OnAdClicked;
                    _interstitial.OnAdFullScreenContentOpened += OnAdDisplayOpened;
                    _interstitial.OnAdFullScreenContentFailed += (e) => OnAdDisplayFaild(e.ToString());
                    _interstitial.OnAdFullScreenContentClosed += OnAdDisplayClosed;
                    _interstitial.OnAdImpressionRecorded += OnAdDisplayImpression;
                    OnAdLoaded();
                });
        }

        public override void ShowAd()
        {
            if (CanShowInterstitial())
            {
                _interstitial.Show();
            }
            else
                OnAdDisplayFaild(string.Empty);
        }
    }
}
