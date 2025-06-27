using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManhPackage.Unit
{
    [Serializable]
    public abstract class InterstitialUnit : AdUnit
    {
        public InterstitialUnit(string _adUnit) : base(_adUnit)
        {
        }

        public abstract void LoadAd() ;

        public abstract void ShowAd() ;

        public abstract bool CanShowInterstitial();
    }
}
