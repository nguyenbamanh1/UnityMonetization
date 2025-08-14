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

    public enum BannerType
    {
        Normal,
        Adaptive,
        Collapsible_Bottom,
        Collapsible_Top,
    }

    public enum BannerSize
    {
        Normal,
        Medium,
        IAB,
        CustomSize,
    }

    public class BannerUnit : AdUnit
    {
        [SerializeField] protected BannerPosition position = BannerPosition.Top;
        [SerializeField] protected BannerType type = BannerType.Normal;

        [SerializeField] protected BannerSize sizeType = BannerSize.Normal;
        [SerializeField] protected Vector2Int customSize = default;

        public BannerUnit(string _adUnit, BannerPosition bannerPosition) : base(_adUnit)
        {
            position = bannerPosition;
        }

        public virtual void SetType(BannerType type) => this.type = type;

        public virtual void SetSizeType(BannerSize bannerSize) => this.sizeType = bannerSize;

        public virtual void SetSize(Vector2Int size) => customSize = size;

        public virtual void LoadAd() { }

        public virtual void ShowAd() { }

        public virtual void ShowAd(Vector2Int position) { }

        public virtual void HideAd() { }

        public virtual void DestroyAd() { }
    }
}
