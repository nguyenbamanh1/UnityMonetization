using GoogleMobileAds.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace UnityMonetization.Unit
{
    public enum BannerPosition
    {
        Top,
        Bottom,
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight,
        Center,
        CenterLeft,
        CenterRight,
    }

    [Serializable]
    public class BannerUnit : AdUnit
    {
        [SerializeField] protected BannerPosition position = BannerPosition.Top;

        public BannerUnit(string _adUnit, BannerPosition bannerPosition) : base(_adUnit)
        {
            position = bannerPosition;
        }

        public virtual void LoadAd() { }

        public virtual void ShowAd() { }

        public virtual void ShowAd(Vector2Int position) { }

        public virtual void HideAd() { }

        public virtual void DestroyAd() { }
    }
}
