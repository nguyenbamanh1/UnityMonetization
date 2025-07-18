using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UnityMonetization.Unit.Max
{
    [Serializable]
    public class BannerMaxUnit : BannerUnit, IMaxListener
    {
        public BannerMaxUnit(string _adUnit, BannerPosition bannerPosition) : base(_adUnit, bannerPosition)
        {
            MaxSdkCallbacks.Banner.OnAdLoadedEvent += OnMaxAdLoaded;
            MaxSdkCallbacks.Banner.OnAdLoadFailedEvent += OnMaxAdLoadFaild;
            MaxSdkCallbacks.Banner.OnAdClickedEvent += OnMaxAdClicked;
            MaxSdkCallbacks.Banner.OnAdRevenuePaidEvent += OnMaxAdPaid;
        }

        public override void ShowAd()
        {
            if (status == AdStatus.Ready)
                MaxSdk.ShowBanner(_adUnit);
            else
                LoadAd();
        }

        public override void DestroyAd()
        {
            MaxSdk.DestroyBanner(_adUnit);
            MaxSdkCallbacks.Banner.OnAdLoadedEvent -= OnMaxAdLoaded;
            MaxSdkCallbacks.Banner.OnAdLoadFailedEvent -= OnMaxAdLoadFaild;
            MaxSdkCallbacks.Banner.OnAdClickedEvent -= OnMaxAdClicked;
            MaxSdkCallbacks.Banner.OnAdRevenuePaidEvent -= OnMaxAdPaid;
            status = AdStatus.None;
        }

        public override void HideAd()
        {
            MaxSdk.HideBanner(_adUnit);
        }

        public override void LoadAd()
        {
            if (status == AdStatus.Loading || status == AdStatus.Ready) return;
            status = AdStatus.Loading;
            var _position = position switch
            {
                BannerPosition.Top => MaxSdk.BannerPosition.TopCenter,
                BannerPosition.Bottom => MaxSdk.BannerPosition.BottomCenter,
                BannerPosition.TopLeft => MaxSdk.BannerPosition.TopLeft,
                BannerPosition.TopRight => MaxSdk.BannerPosition.TopRight,
                BannerPosition.BottomLeft => MaxSdk.BannerPosition.BottomLeft,
                BannerPosition.BottomRight => MaxSdk.BannerPosition.BottomRight,
                BannerPosition.Center => MaxSdk.BannerPosition.Centered,
                BannerPosition.CenterLeft => MaxSdk.BannerPosition.CenterLeft,
                BannerPosition.CenterRight => MaxSdk.BannerPosition.CenterRight,
            };
            MaxSdk.CreateBanner(_adUnit, _position);
        }

        public override void ShowAd(Vector2Int position)
        {
            if (status == AdStatus.Ready)
            {
                MaxSdk.UpdateBannerPosition(_adUnit, position.x, position.y);
                MaxSdk.ShowBanner(_adUnit);
            }
            else
                LoadAd();
        }

        public void OnMaxAdLoaded(string adUnit, MaxSdkBase.AdInfo adInfo) => OnAdLoaded();

        public void OnMaxAdLoadFaild(string adUnit, MaxSdkBase.ErrorInfo adInfo) => OnAdLoadFaild(adInfo.ToString());

        public void OnMaxAdClicked(string adUnit, MaxSdkBase.AdInfo adInfo) => OnAdClicked();

        public void OnMaxAdPaid(string adUnit, MaxSdkBase.AdInfo adInfo) => OnAdPaid(new PaidFormat(adUnit, adInfo.Revenue));

        public void OnMaxAdDisplayed(string adUnit, MaxSdkBase.AdInfo adInfo)
        {
        }

        public void OnMaxAdDisplayClosed(string adUnit, MaxSdkBase.AdInfo adInfo)
        {
        }

        public void OnMaxAdDisplayFailed(string adUnit, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
        {
        }

        public void OnAdHiddenEvent(string adUnit, MaxSdkBase.AdInfo adInfo)
        {
        }
    }
}
