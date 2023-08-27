using Controllers.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UpgradeButtonUI : MonoBehaviour
    {
        [SerializeField] private Button upgradeButton;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI priceText;

        public Button UpgradeButton => upgradeButton;
    
        private UpgradeButton _upgradeButton;
        private UpgradeType _upgradeType;
        private void Awake()
        {
            _upgradeButton = GetComponent<UpgradeButton>();

            _upgradeType = _upgradeButton.UpgradeType;
        }
    
        private void Start()
        {
            UpdateTexts();
        }
        public void UpdateTexts()
        {
            switch (_upgradeType)
            {
                case UpgradeType.Income:
                    levelText.text = "LEVEL " + DataManager.Instance.Income.ToString();
                    priceText.text = (DataManager.Instance.Income*50).ToString();
                    break;
                case UpgradeType.Hose:
                    levelText.text = "LEVEL " + DataManager.Instance.Hose.ToString();
                    priceText.text = (DataManager.Instance.Hose*50).ToString();
                    break;
                case UpgradeType.Damage:
                    levelText.text = "LEVEL " + DataManager.Instance.Damage.ToString();
                    priceText.text = (DataManager.Instance.Damage*50).ToString();
                    break;
                case UpgradeType.FireRate:
                    levelText.text = "LEVEL " + DataManager.Instance.FireRate.ToString();
                    priceText.text = (DataManager.Instance.FireRate*50).ToString();
                    break;
                default:
                    Debug.LogWarning($"Cant find type of {_upgradeType}");
                    break;
            }
        
        }
    
    
    }
}
