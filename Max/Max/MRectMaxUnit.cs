using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace UnityMonetization.Unit.Max
{
    [Serializable]
    public class MRectMaxUnit : MRectUnit, IMaxListener
    {
        public MRectMaxUnit(string _adUnit, BannerPosition bannerPosition) : base(_adUnit, bannerPosition)
        {
            MaxSdkCallbacks.MRec.OnAdLoadedEvent += OnMaxAdLoaded;
            MaxSdkCallbacks.MRec.OnAdLoadFailedEvent += OnMaxAdLoadFaild;
            MaxSdkCallbacks.MRec.OnAdClickedEvent += OnMaxAdClicked;
            MaxSdkCallbacks.MRec.OnAdRevenuePaidEvent += OnMaxAdPaid;
        }

        public override void DestroyAd()
        {
            MaxSdk.DestroyMRec(_adUnit);
            MaxSdkCallbacks.MRec.OnAdLoadedEvent -= OnMaxAdLoaded;
            MaxSdkCallbacks.MRec.OnAdLoadFailedEvent -= OnMaxAdLoadFaild;
            MaxSdkCallbacks.MRec.OnAdClickedEvent -= OnMaxAdClicked;
            MaxSdkCallbacks.MRec.OnAdRevenuePaidEvent -= OnMaxAdPaid;
            status = AdStatus.None;
        }

        public override void HideAd()
        {
            MaxSdk.DestroyMRec(_adUnit);
        }

        public override void LoadAd()
        {
            if (status == AdStatus.Loading || status == AdStatus.Ready) return;
            status = AdStatus.Loading;
            var _position = position switch
            {
                BannerPosition.Top => MaxSdk.AdViewPosition.TopCenter,
                BannerPosition.Bottom => MaxSdk.AdViewPosition.BottomCenter,
                BannerPosition.TopLeft => MaxSdk.AdViewPosition.TopLeft,
                BannerPosition.TopRight => MaxSdk.AdViewPosition.TopRight,
                BannerPosition.BottomLeft => MaxSdk.AdViewPosition.BottomLeft,
                BannerPosition.BottomRight => MaxSdk.AdViewPosition.BottomRight,
                BannerPosition.Center => MaxSdk.AdViewPosition.Centered,
                BannerPosition.CenterLeft => MaxSdk.AdViewPosition.CenterLeft,
                BannerPosition.CenterRight => MaxSdk.AdViewPosition.CenterRight,
            };
            MaxSdk.CreateMRec(_adUnit, _position);
        }

        public override void ShowAd()
        {
            if (status == AdStatus.Ready)
                MaxSdk.ShowMRec(_adUnit);
            else
                LoadAd();
        }

        public override void ShowAd(Vector2Int position)
        {
            if (status == AdStatus.Ready)
            {
                MaxSdk.UpdateMRecPosition(_adUnit, position.x, position.y);
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
