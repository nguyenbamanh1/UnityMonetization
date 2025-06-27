using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManhPackage.Unit.Max
{
    [Serializable]
    public class AOAMaxUnit : AppOpenUnit
    {
        public AOAMaxUnit(string _adUnit) : base(_adUnit)
        {
            MaxSdkCallbacks.AppOpen.OnAdHiddenEvent += (s, a) => OnAdDisplayClosed();
            MaxSdkCallbacks.AppOpen.OnAdRevenuePaidEvent += (s, a) => OnAdPaid(new PaidFormat(_adUnit, a.Revenue));
            MaxSdkCallbacks.AppOpen.OnAdClickedEvent += (s, a) => OnAdClicked();
            MaxSdkCallbacks.AppOpen.OnAdLoadedEvent += (s, a) => OnAdLoaded();
            MaxSdkCallbacks.AppOpen.OnAdLoadFailedEvent += (s, e) => OnAdLoadFaild(e.ToString());
            MaxSdkCallbacks.AppOpen.OnAdDisplayFailedEvent += (s, e, a) => OnAdDisplayFaild(e.ToString());
            MaxSdkCallbacks.AppOpen.OnAdDisplayedEvent += (s, a) => OnAdDisplayOpened();
        }

        public override bool CanShow()
        {
            return MaxSdk.IsAppOpenAdReady(_adUnit);
        }

        public override void LoadAd()
        {
            if (!MaxSdk.IsAppOpenAdReady(_adUnit))
            {
                status = AdStatus.Loading;
                MaxSdk.LoadAppOpenAd(_adUnit);
            }
        }

        public override void ShowAd()
        {
            if (MaxSdk.IsAppOpenAdReady(_adUnit))
                MaxSdk.ShowAppOpenAd(_adUnit);
            else
                LoadAd();
        }
    }
}
