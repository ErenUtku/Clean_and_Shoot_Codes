using System;
using UnityEngine;

namespace Obstacles.Door
{
    [RequireComponent(typeof(DoorUI),typeof(DoorMaterials),typeof(DoorObjects))]
    [RequireComponent(typeof(DoorMovement))]
    public class Door : DamageableTypes, IDamageable
    {
        [Header("Door Details")] 
        [SerializeField] private BuffType buffType;
        [SerializeField] private float buffAmount;
        [SerializeField] private bool isLocked;
        
        #region PRIVATE FIELDS
        //Data
        private float _lockHealth = 5;
        
        //Components
        private DoorUI _doorUI;
        private DoorMaterials _doorMaterial;
        private DoorObjects _doorObjects;

        #endregion

        #region ACTIONS

        public static Action<float, BuffType> ApplyBuff;

        #endregion
        
        private void Awake()
        {
            _doorUI = GetComponent<DoorUI>();
            _doorMaterial = GetComponent<DoorMaterials>();
            _doorObjects = GetComponent<DoorObjects>();
        }

        private void Start()
        {
            _doorUI.UpdateDoorTypeUI(buffType);
            _doorUI.UpdateAmountUI(buffAmount);
            _doorMaterial.MaterialColorUpdate(buffAmount);
            
            _doorObjects.DoorLockActivation(isLocked);
        }

        public void Damage(float amount)
        {
            if (isLocked)
            {
                _lockHealth -= amount;

                if (_lockHealth <= 0)
                {
                    isLocked = false;
                    _doorObjects.DoorLockActivation(isLocked);
                }
            }
            else
            {

                BuffCalculation(amount);
                
                _doorUI.UpdateAmountUI(buffAmount);
                _doorMaterial.MaterialColorUpdate(buffAmount);
            }
        }

        public void CollisionBehavior()
        {
            if (isLocked)
            {
                _lockHealth = 0;
                isLocked = false;
                _doorObjects.DoorLockActivation(isLocked);
                KnockPlayer?.Invoke();
            }
            else
            {
                switch (buffType)
                {
                    case BuffType.Damage:
                        ApplyBuff?.Invoke(buffAmount, BuffType.Damage);
                        break;
                    case BuffType.FireRate:
                        ApplyBuff?.Invoke(buffAmount, BuffType.FireRate);
                        break;
                    default:
                        Debug.LogWarning($"There is no such a type called{buffType}");
                        break;
                }
                
                Destroy(this.gameObject);
            }
        }

        public bool CheckLockCondition()
        {
            return isLocked;
        }

        private void BuffCalculation(float amount)
        {
            buffAmount += (amount/10);
        }
    }
}
