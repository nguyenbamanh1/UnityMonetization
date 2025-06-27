using GoogleMobileAds.Common;
using ManhhdcPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ManhPackage.Unit
{
    [Serializable]
    public class AdUnit
    {
        protected string _adUnit;

        public AdStatus status = AdStatus.None;
        public event Action OnAdLoadedEvent;
        public event Action<string> OnAdLoadFaildEvent;
        public event Action OnAdClickedEvent;
        public event Action<PaidFormat> OnAdPaidEvent;
        public event Action OnAdDisplayOpenedEvent;
        public event Action<string> OnAdDisplayFaildEvent;
        public event Action OnAdDisplayClosedEvent;

        public AdUnit(string _adUnit) { this._adUnit = _adUnit; }

        protected void OnAdLoaded()
        {
            status = AdStatus.Ready;
            GameDispatcher.Instance.ExecuteOnUpdate(() => OnAdLoadedEvent?.Invoke());
        }

        protected void OnAdLoadFaild(string err)
        {
            status = AdStatus.None;
            GameDispatcher.Instance.ExecuteOnUpdate(() => OnAdLoadFaildEvent?.Invoke(err));
        }

        protected void OnAdClicked() => GameDispatcher.Instance.ExecuteOnUpdate(() => OnAdClickedEvent?.Invoke());

        protected void OnAdPaid(PaidFormat paid) => GameDispatcher.Instance.ExecuteOnUpdate(() => OnAdPaidEvent?.Invoke(paid));

        protected void OnAdDisplayOpened() => GameDispatcher.Instance.ExecuteOnUpdate(() => OnAdDisplayOpenedEvent?.Invoke());

        protected void OnAdDisplayFaild(string e) => GameDispatcher.Instance.ExecuteOnUpdate(() => OnAdDisplayFaildEvent?.Invoke(e));

        protected void OnAdDisplayClosed() => GameDispatcher.Instance.ExecuteOnUpdate(() => OnAdDisplayClosedEvent?.Invoke());
    }
}
