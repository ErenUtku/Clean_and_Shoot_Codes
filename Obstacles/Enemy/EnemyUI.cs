namespace Obstacles.Enemy
{
    public class EnemyUI : DamageableUI
    {
        public void UpdateEnemyHealthUI(float amount)
        {
            mainAmountText.text = ((int)amount).ToString();

            if (amount <= 0)
            {
                MainTextActivation(false);
            }
        }

        public void MainTextActivation(bool value)
        {
            mainAmountText.gameObject.SetActive(value);
        }
    }
}
