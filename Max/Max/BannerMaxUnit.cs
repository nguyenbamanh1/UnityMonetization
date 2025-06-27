using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManhPackage.Unit.Max
{
    [Serializable]
    public class BannerMaxUnit : BannerUnit
    {
        public BannerMaxUnit(string _adUnit, BannerPosition bannerPosition) : base(_adUnit, bannerPosition)
        {
            MaxSdkCallbacks.Banner.OnAdLoadedEvent += (s, a) => OnAdLoaded();
            MaxSdkCallbacks.Banner.OnAdLoadFailedEvent += (s, a) => OnAdLoadFaild(a.ToString());
            MaxSdkCallbacks.Banner.OnAdClickedEvent += (s, a) => OnAdClicked();
            MaxSdkCallbacks.Banner.OnAdRevenuePaidEvent += (s, a) => OnAdPaid(new PaidFormat(_adUnit, a.Revenue));
        }

        public override void DestroyAd()
        {
            MaxSdk.DestroyBanner(_adUnit);
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

        public override void ShowAd()
        {
            if (status == AdStatus.Ready)
                MaxSdk.ShowBanner(_adUnit);
            else
                LoadAd();
        }
    }
}
