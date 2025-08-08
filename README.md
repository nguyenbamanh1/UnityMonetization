# UnityMonetization

**UnityMonetization** là một thư viện C# giúp nhà phát triển Unity dễ dàng tích hợp và chuyển đổi giữa nhiều mạng quảng cáo khác nhau như **AdMob**, **AppLovin MAX**,... chỉ với một giao diện thống nhất.
Dự án được thiết kế để tiết kiệm thời gian tích hợp và tối ưu hóa luồng doanh thu quảng cáo trong game/app Unity.

---

## 🚀 Tính năng nổi bật

* Hỗ trợ nhiều mạng quảng cáo: **AdMob 9.2.0**, **MAX SDK 8.x**, v.v.
* Thiết kế module mở rộng, dễ dàng thêm hoặc chuyển đổi network.
* Hoạt động với **mọi phiên bản Unity** (thư viện độc lập .dll).
* Tích hợp nhanh chóng chỉ với vài bước đơn giản.
* API thống nhất, giúp giảm thiểu việc viết lại mã khi thay đổi nền tảng quảng cáo.

---

## 📦 Cài đặt

### Bước 1: Tải thư viện

Clone hoặc tải release `.dll` từ repo:

```bash
git clone https://github.com/nguyenbamanh1/UnityMonetization.git
```

Hoặc tải file `.dll` từ mục [Releases](https://github.com/nguyenbamanh1/UnityMonetization/releases).

### Bước 2: Thêm vào Unity

1. Mở dự án Unity của bạn.
2. Tạo thư mục `Assets/Plugins/UnityMonetization/` (nếu chưa có).
3. Thêm các file `.dll` vào thư mục trên.
4. Unity sẽ tự động nhận diện và load thư viện.

> ⚠️ Đối với Max và IronSource không thể chạy cùng 1 lúc 2 mạng cùng lúc bạn hãy tắt tuỳ chọn sử dụng `Max.dll` hoặc `IronSource.dll` trong Unity
---

## 🛠️ Yêu cầu

* Unity: không giới hạn phiên bản (cần hỗ trợ `.dll`)
* Các SDK tương ứng của nền tảng quảng cáo:

  * AdMob SDK: `v9.2.0`
  * AppLovin MAX SDK: `8.x`

> ⚠️ Bạn cần tự tích hợp SDK gốc của từng network theo hướng dẫn riêng của họ (UnityMonetization không bao gồm sẵn SDK gốc).

---
> ⚠️ Đối với mạng `Admob` BannerPosition không hỗ trợ CenterLeft và CenterRight. MRect admob thực chất chỉ là banner ở dạng `MediumRectangle` `(300, 250)`

> ⚠️ Đối với `Admob` kiểu `Adaptive` size sẽ được tự động hoá và `sizeType` sẽ không hoạt động

> ⚠️ Đối với mạng `Max` Banner chỉ hỗ trợ `Normal` và `Adaptive`. Và banner không hỗ trợ `custom size`, chỉ nên dùng size `normal`

> ⚠️ Đối với mạng `IronSource` không hỗ trợ `AOA unit`.

```csharp
using UnityMonetization;


public enum AdUnitType
{
    MAX,
    Admob,
    IronSource
}

public class AdsExample : MonoBehaviour
{

    [Header("Unit Config")]
    [SerializeField] protected string _unitBannerId;
    [SerializeField] private AdUnitType _bannerUnitType = AdUnitType.MAX;
    [SerializeField] private BannerPosition _bannerPosition = BannerPosition.Top;
    
    [Space(10f)]
    [SerializeField] protected string _unitAOAId;
    [SerializeField] private AdUnitType _appOpenUnitType = AdUnitType.MAX;

    [Space(10f)]
    [SerializeField] protected string _unitInterId;
    [SerializeField] private AdUnitType _interUnitType = AdUnitType.MAX;

    [Space(10f)]
    [SerializeField] protected string _unitRewardId;
    [SerializeField] private AdUnitType _rewardUnitType = AdUnitType.MAX;

    BannerUnit _bannerUnit;
    AppOpenUnit _appOpenUnit;
    InterstitialUnit _interstitialUnit;
    RewardUnit _rewardUnit;

    /// <summary>
    /// Event được gọi khi MobileAds.Initialize thành công
    /// </summary>
    event Action _InitializeAdmobEvent;

    /// <summary>
    /// Event được gọi khi callback MaxSdkCallbacks.OnSdkInitializedEvent được gọi
    /// </summary>
    event Action _InitializeMaxEvent;
    void Start()
    {
        GenerateUnit();

        //Listen Event cho các đơn vị quảng cáo
        ...
    
        //kiểm tra xem có đơn vị quảng cáo nào là của Max không, nếu có thì sẽ gọi MaxSdk.InitializeSdk
        if (_InitializeMaxEvent != null)
        {
            MaxSdk.SetVerboseLogging(true);
            MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) =>
            {
                _InitializeMaxEvent?.Invoke();
                _maxInitialized = true;
            };
            MaxSdk.SetUserId("USER_ID");
            MaxSdk.InitializeSdk();
        }

        //Initialize Admob
        AdmobInitialized();
    }
    
    private void AdmobInitialized()
    {
        if (!_admobInitialized)
        {
            MobileAds.Initialize((initStatus) =>
            {
                _InitializeAdmobEvent?.Invoke();
                LoadNativeAd();
                _InitializeAdmobEvent = null;
                _admobInitialized = true;
            });
        }
    }
    
    private void GenerateBanner()
    {
        Type type = null;
        switch (_bannerUnitType)
        {
            case AdUnitType.MAX:
                _bannerUnit = new BannerMaxUnit(_unitBannerId, _bannerPosition);
                break;
            case AdUnitType.Admob:
                _bannerUnit = new BannerAmobUnit(_unitBannerId, _bannerPosition);
                break;
            case AdUnitType.IronSource:
                _bannerUnit = new BannerIronUnit(_unitBannerId, _bannerPosition);
                break;
        }
        if (!string.IsNullOrEmpty(_unitBannerId))
        {
            if (_bannerUnitType == AdUnitType.MAX)
                _InitializeMaxEvent += LoadBanner;
            else
                _InitializeAdmobEvent += LoadBanner;
        }
    }
    
    private void GenerateAOA()
    {
        switch (_appOpenUnitType)
        {
            case AdUnitType.MAX:
                _appOpenUnit = new AOAMaxUnit(_unitAOAId);
                break;
            case AdUnitType.Admob:
                _appOpenUnit = new AOAAdmobUnit(_unitAOAId);
                break;
            case AdUnitType.IronSource:
                break;
        }
        if (!string.IsNullOrEmpty(_unitAOAId))
        {
            if (_appOpenUnitType == AdUnitType.MAX)
                _InitializeMaxEvent += LoadAOA;
            else
                _InitializeAdmobEvent += LoadAOA;
        }
    }
    
    private void GenerateInterstitial()
    {
        switch (_interUnitType)
        {
            case AdUnitType.MAX:
                _interstitialUnit = new InterstitialMaxUnit(_unitInterId);
                break;
            case AdUnitType.Admob:
                _interstitialUnit = new InterstitialAdmobUnit(_unitInterId);
                break;
            case AdUnitType.IronSource:
                _interstitialUnit = new InterstitialIronUnit(_unitInterId);
                break;
        }
        if (!string.IsNullOrEmpty(_unitInterId))
        {
            if (_interUnitType == AdUnitType.MAX)
                _InitializeMaxEvent += LoadInterstitial;
            else
                _InitializeAdmobEvent += LoadInterstitial;
        }
    }
    
    private void GenerateReward()
    {
        switch (_rewardUnitType)
        {
            case AdUnitType.MAX:
                _rewardUnit = new RewardMaxUnit(_unitRewardId);
                break;
            case AdUnitType.Admob:
                _rewardUnit = new RewardAdmobUnit(_unitRewardId);
                break;
            case AdUnitType.IronSource:
                _rewardUnit = new RewardIronUnit(_unitRewardId);
                break;
        }
        if (!string.IsNullOrEmpty(_unitRewardId))
        {
            if (_rewardUnitType == AdUnitType.MAX)
                _InitializeMaxEvent += LoadReward;
            else
                _InitializeAdmobEvent += LoadReward;
        }
    }
    
    private void GenerateUnit()
    {
        GenerateBanner();
    
        GenerateAOA();
    
        GenerateInterstitial();
    
        GenerateReward();
    }
}
```

## 📁 Cấu trúc thư viện

```
UnityMonetization/
│
├── Core/              # Giao diện và lớp chung
├── Admob/             # Cài đặt và adapter cho AdMob
├── Max/               # Cài đặt và adapter cho AppLovin MAX
└── IronSource/             # Các tiện ích hỗ trợ
├── UnityMonetization/
```

---

## 🤝 Đóng góp

Bạn có thể đóng góp bằng cách:

* Gửi pull request cải thiện thư viện
* Mở issue nếu phát hiện lỗi
* Góp ý về cách tích hợp mạng quảng cáo khác

---

## 📄 License

[MIT License](LICENSE)

---

## 📢 Liên hệ

Tác giả: **Nguyễn Bá Mạnh**
GitHub: [@nguyenbamanh1](https://github.com/nguyenbamanh1)

---
