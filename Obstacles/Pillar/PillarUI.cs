
using System;

namespace Obstacles.Pillar
{
    public class PillarUI : DamageableUI
    {

        public void UpdateHealthText(float healthAmount)
        {
            mainAmountText.text = ((int) healthAmount).ToString();
        }
    }
}
