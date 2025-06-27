using GoogleMobileAds.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManhPackage.Unit.Admob
{
    public class AOAAdmobUnit : AppOpenUnit
    {
        AppOpenAd _aoa;

        public AOAAdmobUnit(string _adUnit) : base(_adUnit)
        {
        }

        public override bool CanShow()
        {
            return _aoa != null && _aoa.CanShowAd();
        }

        public override void LoadAd()
        {
            if (status == AdStatus.Loading)
                return;
            status = AdStatus.Loading;
            AppOpenAd.Load(_adUnit, new AdRequest(), (ad, err) =>
            {
                if (err != null)
                {
                    OnAdLoadFaild(err.ToString());
                    return;
                }
                _aoa = ad;

                _aoa.OnAdPaid += (v) => OnAdPaid(new PaidFormat(_adUnit, v.CurrencyCode, v.Value));
                _aoa.OnAdClicked += OnAdClicked;
                _aoa.OnAdFullScreenContentClosed += OnAdDisplayClosed;
                _aoa.OnAdFullScreenContentOpened += OnAdDisplayOpened;
                _aoa.OnAdFullScreenContentFailed += (v) => OnAdLoadFaild(v.ToString());
                OnAdLoaded();
            });
        }

        public override void ShowAd()
        {
            if (_aoa != null && _aoa.CanShowAd())
            {
                _aoa.Show();
            }
            else
            {
                LoadAd();
            }
        }
    }
}
