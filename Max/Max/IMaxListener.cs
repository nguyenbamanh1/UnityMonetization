using System;
using System.Collections.Generic;
using System.Text;

namespace UnityMonetization.Unit.Max
{
    public interface IMaxListener
    {
        void OnMaxAdLoaded(string adUnit, MaxSdkBase.AdInfo adInfo);
        void OnMaxAdLoadFaild(string adUnit, MaxSdkBase.ErrorInfo adInfo);
        void OnMaxAdClicked(string adUnit, MaxSdkBase.AdInfo adInfo);
        void OnMaxAdPaid(string adUnit, MaxSdkBase.AdInfo adInfo);

        void OnMaxAdDisplayed(string adUnit, MaxSdkBase.AdInfo adInfo);

        void OnMaxAdDisplayClosed(string adUnit, MaxSdkBase.AdInfo adInfo);

        void OnMaxAdDisplayFailed(string adUnit, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo);

        void OnAdHiddenEvent(string adUnit, MaxSdkBase.AdInfo adInfo);
    }
}
