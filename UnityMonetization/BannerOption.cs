using System.Collections;
using UnityEngine;
using UnityMonetization.Unit;

namespace UnityMonetization
{
    [System.Serializable]
    public class BannerOption : AdOption
    {
        [SerializeField] protected BannerPosition position = BannerPosition.Top;
        [SerializeField] protected BannerType type = BannerType.Normal;

        [SerializeField] protected BannerSize sizeType = BannerSize.Normal;
        [SerializeField] protected Vector2Int customSize = default;

        public BannerPosition Position => position;

        public BannerType Type => type;

        public BannerSize Size => sizeType;

        public Vector2Int CustomSize => customSize;
    }
}