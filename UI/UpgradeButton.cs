using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(UpgradeButtonUI),typeof(UpgradeButtonPurchase))]
    public class UpgradeButton : MonoBehaviour
    {
        [SerializeField] private UpgradeType upgradeType;
    
        public UpgradeType UpgradeType=>upgradeType;

        private UpgradeButtonUI _upgradeButtonUI;
        private UpgradeButtonPurchase _upgradeButtonPurchase;

        private void Awake()
        {
            _upgradeButtonUI = GetComponent<UpgradeButtonUI>();
            _upgradeButtonPurchase = GetComponent<UpgradeButtonPurchase>();
        }

        private void Start()
        {
            _upgradeButtonUI.UpgradeButton.onClick?.AddListener(_upgradeButtonPurchase.PurchaseItem);
        }
    }

    public enum UpgradeType
    {
        Income,
        Hose,
        Damage,
        FireRate
    }
}