using UnityEngine;

namespace Obstacles.Enemy
{
    public class EnemyObjects : DamageableObjects
    {
        [SerializeField] private GameObject negativeEnemy;
        public void EnemyColliderActivation(bool value)
        {
            mainObject.GetComponent<BoxCollider>().enabled = value;
        }
        
        public void NegativeEnemyActivation(bool value)
        {
            negativeEnemy.SetActive(value);
        }
    }
}
