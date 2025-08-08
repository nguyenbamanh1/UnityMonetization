using GoogleMobileAds.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UnityMonetization.Unit.Admob
{
    [Serializable]
    public class BannerAdmobUnit : BannerUnit
    {
        BannerView _banner;
        public BannerAdmobUnit(string _adUnit, BannerPosition bannerPosition) : base(_adUnit, bannerPosition)
        {
        }

        string GetNewUUID()
        {
            Guid myuuid = Guid.NewGuid();
            string myuuidAsString = myuuid.ToString();
            return myuuidAsString;
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

            AdSize adSize = AdSize.Banner;

            switch (sizeType)
            {
                case BannerSize.Normal:
                case BannerSize.Medium:
                    adSize = AdSize.MediumRectangle;
                    break;
                case BannerSize.IAB:
                    adSize = AdSize.IABBanner;
                    break;
                case BannerSize.CustomSize:
                    adSize = new AdSize(customSize.x, customSize.y);
                    break;
            }

            var adRequest = new AdRequest();
            switch (type)
            {
                case BannerType.Adaptive:
                    adSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
                    break;
                case BannerType.Collapsible_Bottom:
                    adRequest.Extras.Add("collapsible", "bottom");
                    adRequest.Extras.Add("collapsible_request_id", GetNewUUID());
                    break;
                case BannerType.Collapsible_Top:
                    adRequest.Extras.Add("collapsible", "top");
                    adRequest.Extras.Add("collapsible_request_id", GetNewUUID());
                    break;
            }

            if ((byte)position <= 6)
                _banner = new BannerView(_adUnit, adSize, (AdPosition)position);
            else
            {
                Vector2Int pos = default;
                switch (position)
                {
                    case BannerPosition.CenterRight:
                        adSize = new AdSize(adSize.Height, adSize.Width);
                        pos.x = Screen.width - adSize.Width;
                        pos.y = (Screen.height - adSize.Height) / 2;
                        break;
                    case BannerPosition.CenterLeft:
                        adSize = new AdSize(adSize.Height, adSize.Width);
                        pos.x = 0;
                        pos.y = (Screen.height - adSize.Height) / 2;
                        break;
                }
                _banner = new BannerView(_adUnit, adSize, pos.x, pos.y);
            }
            _banner.OnBannerAdLoaded += OnAdLoaded;
            _banner.OnBannerAdLoadFailed += (err) => OnAdLoadFaild(err.ToString());
            _banner.OnAdClicked += OnAdClicked;
            _banner.OnAdPaid += (v) => OnAdPaid(new PaidFormat(_adUnit, v.CurrencyCode, v.Value));
            _banner.OnAdImpressionRecorded += OnAdDisplayImpression;
            _banner.LoadAd(adRequest);
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
                _banner.SetPosition((int)position.x, (int)position.y);
                _banner.Show();
            }
            else
            {
                LoadAd();

            }
        }
    }
}
