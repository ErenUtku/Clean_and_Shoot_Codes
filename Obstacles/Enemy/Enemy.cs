using System;
using UnityEngine;
using Utils;

namespace Obstacles.Enemy
{
    [RequireComponent(typeof(EnemyUI),typeof(EnemyMaterials),typeof(EnemyObjects))]
    [RequireComponent(typeof(EnemyAnimator))]
    public class Enemy : DamageableTypes,IDamageable
    {
        [SerializeField] private float healthAmount =100f;
        
        private EnemyUI _enemyUI;
        private EnemyMaterials _enemyMaterials;
        private EnemyObjects _enemyObjects;
        private EnemyAnimator _enemyAnimator;

        private float _defaultHealth;
        private Exchanger _exchanger;
        
        private void Awake()
        {
            _enemyUI = GetComponent<EnemyUI>();
            _enemyMaterials = GetComponent<EnemyMaterials>();
            _enemyObjects = GetComponent<EnemyObjects>();
            _enemyAnimator = GetComponent<EnemyAnimator>();

            _defaultHealth = healthAmount;
            
        }

        private void Start()
        {
            _exchanger = new Exchanger();
            
            _enemyMaterials.UpdateMaskedObjectPosition(healthAmount,_defaultHealth);
            _enemyUI.UpdateEnemyHealthUI(healthAmount);
        }

        public void Damage(float amount)
        {
            _enemyAnimator.TriggerHit();
            
            healthAmount -= amount;
            
            _enemyUI.UpdateEnemyHealthUI(healthAmount);


            if (healthAmount <= 0)
            {
                _enemyAnimator.KillCharacter();
                
                _enemyObjects.EnemyColliderActivation(false);

                _enemyObjects.NegativeEnemyActivation(false);
                
                _exchanger.AddCurrency(100,PlayerDataType.Coin); //100 is fixed for now
                
                Destroy(this.gameObject,3f);
            }
            
            _enemyMaterials.UpdateMaskedObjectPosition(healthAmount,_defaultHealth);
        }

        public void CollisionBehavior()
        {
            _enemyUI.MainTextActivation(false);
            
            _enemyAnimator.KillCharacter();
            
            _enemyObjects.NegativeEnemyActivation(false);
            
            _enemyObjects.EnemyColliderActivation(false);
            
            _enemyMaterials.UpdateMaskedObjectPosition(0,_defaultHealth);

            KnockPlayer?.Invoke();
            
            Destroy(this.gameObject,3f);
        }
    }
}
