using GoogleMobileAds.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.IO.LowLevel.Unsafe;

namespace ManhPackage.Unit
{
    [Serializable]
    public abstract class RewardUnit : AdUnit
    {
        public RewardUnit(string _adUnit) : base(_adUnit)
        {
        }

        public abstract void LoadAd();

        public abstract void ShowAd(Action rewardAction);
    }
}
