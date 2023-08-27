using System;
using Controllers;
using Controllers.State;
using UnityEngine;
using Utils;

namespace Obstacles.Pillar
{
    
    [RequireComponent(typeof(PillarUI),typeof(PillarMaterials),typeof(PillarObjects))]
    public class Pillar : DamageableTypes, IDamageable
    {
        [SerializeField] private float healthAmount;
        //Components
        private PillarUI _pillarUI;
        private PillarMaterials _pillarMaterial;
        private PillarObjects _pillarObjects;

        private Exchanger _exchanger;
        private void Awake()
        {
            _pillarUI = GetComponent<PillarUI>();
            _pillarMaterial = GetComponent<PillarMaterials>();
            _pillarObjects = GetComponent<PillarObjects>();
        }

        private void Start()
        {
            _exchanger = new Exchanger();
            
            _pillarUI.UpdateHealthText(healthAmount);
        }

        public void Damage(float amount)
        {
            healthAmount -= amount;

            if (healthAmount <= 0)
            {
                Destroy(this.gameObject);
                
                _exchanger.AddCurrency(100,PlayerDataType.Coin);
            }

            _pillarUI.UpdateHealthText(healthAmount);
        }

        public void CollisionBehavior()
        {
            //Game is Over
            StateManager.Instance.stateMachine.currentState.ExitState();
            LevelManager.Instance.LevelComplete();
            
        }
    }
}
