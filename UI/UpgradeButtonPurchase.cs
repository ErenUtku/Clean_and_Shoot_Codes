using Controllers.Data;
using UnityEngine;
using Utils;

namespace UI
{
    public class UpgradeButtonPurchase : MonoBehaviour
    {
        private UpgradeButton _upgradeButton;
        private UpgradeButtonUI _upgradeButtonUI;
    
        private Exchanger _exchanger;
        private void Awake()
        {
            _exchanger = new Exchanger();

            _upgradeButton = GetComponent<UpgradeButton>();
            _upgradeButtonUI = GetComponent<UpgradeButtonUI>();
        }
    
        public void PurchaseItem()
        {
            switch (_upgradeButton.UpgradeType)
            {
                case UpgradeType.Income:
                    _exchanger.BuyCustomItem(PlayerDataType.Income,PlayerDataType.Coin,1,(int)DataManager.Instance.Income*50 ,PurchaseSuccessful,PurchaseFail);
                    break;
                case UpgradeType.Hose:
                    _exchanger.BuyCustomItem(PlayerDataType.Hose,PlayerDataType.Coin,1,(int)DataManager.Instance.Hose*50 ,PurchaseSuccessful,PurchaseFail);
                    break;
                case UpgradeType.Damage:
                    _exchanger.BuyCustomItem(PlayerDataType.Damage,PlayerDataType.Coin,1,(int)DataManager.Instance.Damage*50 ,PurchaseSuccessful,PurchaseFail);
                    break;
                case UpgradeType.FireRate:
                    _exchanger.BuyCustomItem(PlayerDataType.FireRate,PlayerDataType.Coin,1,(int)DataManager.Instance.FireRate*50 ,PurchaseSuccessful,PurchaseFail);
                    break;
                default:
                    Debug.LogWarning($"Type is missing {_upgradeButton.UpgradeType}");
                    break;
            }
        }

        private void PurchaseSuccessful()
        {
            _upgradeButtonUI.UpdateTexts();
            //POPUP
        }

        private void PurchaseFail()
        {
            //POPUP
        }

   
    }
}
