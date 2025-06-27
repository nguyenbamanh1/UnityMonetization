using GoogleMobileAds.Api;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ManhPackage.Unit.Admob
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

            if ((byte)position <= 6)
                _banner = new BannerView(_adUnit, AdSize.MediumRectangle, (AdPosition)position);
            else
            {
                Vector2Int pos = default;
                AdSize size = new AdSize(AdSize.MediumRectangle.Width, AdSize.MediumRectangle.Height);
                switch (position)
                {
                    case BannerPosition.CenterRight:
                        size = new AdSize(AdSize.MediumRectangle.Height, AdSize.MediumRectangle.Width);
                        pos.x = Screen.width - size.Width;
                        pos.y = (Screen.height - size.Height) / 2;
                        break;
                    case BannerPosition.CenterLeft:
                        size = new AdSize(AdSize.MediumRectangle.Height, AdSize.MediumRectangle.Width);
                        pos.x = 0;
                        pos.y = (Screen.height - size.Height) / 2;
                        break;
                }

                _banner = new BannerView(_adUnit, size, pos.x, pos.y);
                _banner.OnBannerAdLoaded += OnAdLoaded;
                _banner.OnBannerAdLoadFailed += (err) => OnAdLoadFaild(err.ToString());
                _banner.OnAdClicked += OnAdClicked;
                _banner.OnAdPaid += (v) => OnAdPaid(new PaidFormat(_adUnit, v.CurrencyCode, v.Value));
                _banner.OnAdImpressionRecorded += OnAdDisplayImpression;
            }

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
