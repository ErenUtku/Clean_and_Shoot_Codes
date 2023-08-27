using TMPro;
using UnityEngine;

namespace Obstacles.Door
{
    public class DoorUI : DamageableUI
    {
        [SerializeField] private TextMeshProUGUI doorTypeText;

        public void UpdateDoorTypeUI(BuffType buffType)
        {
            doorTypeText.text = buffType.ToString();
        }
        public void UpdateAmountUI(float value)
        {
            string formattedValue = value.ToString("F2"); 
            
            if (value > 0)
            {
                formattedValue = "+" + formattedValue;
            }
            
            mainAmountText.text = formattedValue;
        }
    }
}
