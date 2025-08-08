using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityMonetization.Unit;
using UnityMonetization.Unit.Admob;
using UnityMonetization.Unit.IronSource;
using UnityMonetization.Unit.Max;

namespace UnityMonetization
{
    public enum AdUnitType
    {
        MAX,
        Admob,
        IronSource
    }

    public class UnityMonetizationFactory
    {
        private static readonly Dictionary<AdUnitType, Type> _interFactory = new Dictionary<AdUnitType, Type>()
        {
            {AdUnitType.Admob, typeof(InterstitialAdmobUnit) },
            {AdUnitType.MAX, typeof(InterstitialMaxUnit) },
            {AdUnitType.IronSource, typeof(InterstitialIronUnit) }  
        };

        private static readonly Dictionary<AdUnitType, Type> _bannerFactory = new Dictionary<AdUnitType, Type>()
        {
            {AdUnitType.Admob, typeof(BannerAdmobUnit) },
            {AdUnitType.MAX, typeof(BannerMaxUnit) },
            {AdUnitType.IronSource, typeof(BannerIronUnit) }
        };

        private static readonly Dictionary<AdUnitType, Type> _rewardFactory = new Dictionary<AdUnitType, Type>()
        {
            {AdUnitType.Admob, typeof(RewardAdmobUnit) },
            {AdUnitType.MAX, typeof(RewardMaxUnit) },
            {AdUnitType.IronSource, typeof(RewardIronUnit) }
        };

        private static readonly Dictionary<AdUnitType, Type> _aoaFactory = new Dictionary<AdUnitType, Type>()
        {
            {AdUnitType.Admob, typeof(AOAAdmobUnit) },
            {AdUnitType.MAX, typeof(AOAMaxUnit) }
        };


        public static BannerUnit CreateBanner(AdUnitType unitType, params object[] parameters)
        {
            return (BannerUnit)Activator.CreateInstance(_bannerFactory[unitType], parameters);
        }

        public static InterstitialUnit CreateInter(AdUnitType unitType, string adUnitId)
        {
            return (InterstitialUnit)Activator.CreateInstance(_interFactory[unitType], adUnitId);
        }

        public static RewardUnit CreateReward(AdUnitType unitType, string adUnitId)
        {
            return (RewardUnit)Activator.CreateInstance(_rewardFactory[unitType], adUnitId);
        }

        public static AppOpenUnit CreateAOA(AdUnitType adUnitType, string adUnitId)
        {
            if(adUnitType == AdUnitType.IronSource)
            {
                UnityEngine.Debug.LogError("IronSource not support AOA");
                return null;
            }

            return (AppOpenUnit)Activator.CreateInstance(_aoaFactory[adUnitType], adUnitId);
        }
    }
}
