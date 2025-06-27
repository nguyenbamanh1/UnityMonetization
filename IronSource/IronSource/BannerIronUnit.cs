using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Services.LevelPlay;

namespace ManhPackage.Unit.IronSource
{
    public class BannerIronUnit : BannerUnit
    {
        LevelPlayBannerAd _banner = null;

        public BannerIronUnit(string _adUnit, BannerPosition bannerPosition) : base(_adUnit, bannerPosition)
        {
        }

        public override void DestroyAd()
        {
            if (_banner != null && status == AdStatus.Ready)
            {
                _banner.DestroyAd();
                _banner = null;
                status = AdStatus.None;
            }
        }

        public override void HideAd()
        {
            if(_banner != null && status == AdStatus.Ready)
            {
                _banner.HideAd();
            }
        }

        public override void LoadAd()
        {
            if (status == AdStatus.Loading || status == AdStatus.Ready) return;
            status = AdStatus.Loading;
            var position = this.position switch
            {
                BannerPosition.Top => com.unity3d.mediation.LevelPlayBannerPosition.TopCenter,
                BannerPosition.Bottom => com.unity3d.mediation.LevelPlayBannerPosition.BottomCenter,
                BannerPosition.TopLeft => com.unity3d.mediation.LevelPlayBannerPosition.TopLeft,
                BannerPosition.TopRight => com.unity3d.mediation.LevelPlayBannerPosition.TopRight,
                BannerPosition.BottomLeft => com.unity3d.mediation.LevelPlayBannerPosition.BottomLeft,
                BannerPosition.BottomRight => com.unity3d.mediation.LevelPlayBannerPosition.BottomRight,
                BannerPosition.Center => com.unity3d.mediation.LevelPlayBannerPosition.Center,
                BannerPosition.CenterLeft => com.unity3d.mediation.LevelPlayBannerPosition.CenterLeft,
                BannerPosition.CenterRight => com.unity3d.mediation.LevelPlayBannerPosition.CenterRight
            };
            _banner = new LevelPlayBannerAd(_adUnit, size: com.unity3d.mediation.LevelPlayAdSize.BANNER, position, displayOnLoad: false);
            _banner.OnAdLoaded += (i) => OnAdLoaded();
            _banner.OnAdLoadFailed += (e) => OnAdLoadFaild(e.ToString());
            _banner.OnAdDisplayed += (i) => OnAdDisplayOpened();
            _banner.OnAdDisplayFailed += (e) => OnAdDisplayFaild(e.ToString());
            _banner.OnAdClicked += (i) => OnAdClicked();
            _banner.LoadAd();
        }

        public override void ShowAd()
        {
            if (_banner != null && status == AdStatus.Ready)
                _banner.ShowAd();
        }
    }
}
