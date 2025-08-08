using GoogleMobileAds.Api;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using UnityEngine;

namespace UnityMonetization.Unit.Admob
{
    [Serializable]
    public class MRectAdmobUnit : MRectUnit
    {
        BannerView _banner;

        public MRectAdmobUnit(string _adUnit, BannerPosition bannerPosition) : base(_adUnit, bannerPosition)
        {
        }

        public override void DestroyAd()
        {
            if (_banner != null)
            {
                _banner.Destroy();
                _banner = null;
            }
        }

        public override void HideAd()
        {
            if (_banner != null)
            {
                _banner.Hide();
            }
        }

        public override void LoadAd()
        {
            if (status == AdStatus.Loading || status == AdStatus.Ready)
                return;
            status = AdStatus.Loading;

            _banner = new BannerView(_adUnit, AdSize.MediumRectangle, (int)(customSize.x * 160 / Screen.dpi), (int)(customSize.y * 160 / Screen.dpi));
            _banner.OnBannerAdLoaded += OnAdLoaded;
            _banner.OnBannerAdLoadFailed += (err) => OnAdLoadFaild(err.ToString());
            _banner.OnAdClicked += OnAdClicked;
            _banner.OnAdPaid += (v) => OnAdPaid(new PaidFormat(_adUnit, v.CurrencyCode, v.Value));
            _banner.OnAdImpressionRecorded += OnAdDisplayImpression;
            _banner.LoadAd(new AdRequest());
        }

        public override void ShowAd()
        {
            if (_banner != null && status == AdStatus.Ready)
            {
                _banner.Show();
            }
            else
            {
                LoadAd();
            }
        }

        public override void ShowAd(Vector2Int position)
        {
            if (_banner != null && status == AdStatus.Ready)
            {
                _banner.Show();
                _banner.SetPosition(position.x, position.y);
            }
            else
            {
                LoadAd();
            }
        }

    }
}
