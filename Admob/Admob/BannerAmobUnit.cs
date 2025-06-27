using GoogleMobileAds.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ManhPackage.Unit.Admob
{
    [Serializable]
    public class BannerAmobUnit : BannerUnit
    {
        BannerView _banner;
        public BannerAmobUnit(string _adUnit, BannerPosition bannerPosition) : base(_adUnit, bannerPosition)
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
                _banner = new BannerView(_adUnit, AdSize.Banner, (AdPosition)position);
            else
            {
                Vector2Int pos = default;
                AdSize size = new AdSize(AdSize.Banner.Width, AdSize.Banner.Height);
                switch (position)
                {
                    case BannerPosition.CenterRight:
                        size = new AdSize(AdSize.Banner.Height, AdSize.Banner.Width);
                        pos.x = Screen.width - size.Width;
                        pos.y = (Screen.height - size.Height) / 2;
                        break;
                    case BannerPosition.CenterLeft:
                        size = new AdSize(AdSize.Banner.Height, AdSize.Banner.Width);
                        pos.x = 0;
                        pos.y = (Screen.height - size.Height) / 2;
                        break;
                }

                _banner = new BannerView(_adUnit, size, pos.x, pos.y);
            }

            _banner.LoadAd(new AdRequest());
            _banner.OnBannerAdLoaded += OnAdLoaded;
            _banner.OnBannerAdLoadFailed += (err) => OnAdLoadFaild(err.ToString());
            _banner.OnAdClicked += OnAdClicked;
            _banner.OnAdPaid += (v) => OnAdPaid(new PaidFormat(_adUnit, v.CurrencyCode, v.Value));
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
    }
}
