using System.Collections;
using UnityEngine;
using UnityMonetization.Unit;

namespace UnityMonetization
{
    [System.Serializable]
    public abstract class AdOption
    {
        [SerializeField] protected string _adName = string.Empty;
        [SerializeField] protected string _adUnitId = string.Empty;
        [SerializeField] protected AdUnitType _adNetType = AdUnitType.MAX;
        public string AdName => _adName;

        public string AdUnitId => _adUnitId;

        public AdUnitType AdNetType => _adNetType;
    }
}