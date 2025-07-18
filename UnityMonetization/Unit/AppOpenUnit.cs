using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityMonetization.Unit
{
    public abstract class AppOpenUnit : AdUnit
    {
        public AppOpenUnit(string _adUnit) : base(_adUnit)
        {
        }

        public abstract bool CanShow();

        public abstract void LoadAd();

        public abstract void ShowAd();
    }
}
