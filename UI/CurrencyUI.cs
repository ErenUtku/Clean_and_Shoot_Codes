using Controllers.Data;
using TMPro;
using UnityEngine;

namespace UI
{
   public class CurrencyUI : MonoBehaviour
   {
      [SerializeField] private TextMeshProUGUI currencyText;

      private void Awake()
      {
         DataManager.OnDataChanged += UpdateCurrencyText;
      }

      private void Start()
      {
         UpdateCurrencyText(PlayerDataType.Coin);
      }

      private void OnDestroy()
      {
         DataManager.OnDataChanged -= UpdateCurrencyText;
      }
      private void UpdateCurrencyText(PlayerDataType dataType)
      {
         if (dataType == PlayerDataType.Coin)
         {
            currencyText.text = DataManager.Instance.Coin.ToString();
         }
      }
   }
}
