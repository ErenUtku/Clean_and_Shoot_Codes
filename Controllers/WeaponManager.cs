using System;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace Controllers
{
    public class WeaponManager : MonoBehaviour
    {
        [Header("WeaponInformation")] 
        [SerializeField] private Weapon[] allWeapons;
        
        [Header("PlayerInformation")]
        [SerializeField] private Transform playerWeaponContainer;
        
        private List<Transform> _playerWeaponSlots;

        public static WeaponManager Instance;

        #region UNITY EVENTS

        private void Awake()
        {
            Instance = this;
            InitWeaponSlotToPlayer();
        }
        
        #endregion

        #region PRIVATE FIELDS

        private void InitWeaponSlotToPlayer()
        {
            _playerWeaponSlots = new List<Transform>();
            
            foreach (Transform weaponSlot in playerWeaponContainer)
            {
                _playerWeaponSlots.Add(weaponSlot);
            }
        }

        #endregion


        #region PUBLIC FIELDS

        public void AddWeaponToPlayer(Weapon weapon)
        {
            if (_playerWeaponSlots.Count < 0) return;

            var randomSlot = UnityEngine.Random.Range(0, _playerWeaponSlots.Count);
            
            weapon.MovementControl.MoveWeaponToPlayer(_playerWeaponSlots[randomSlot]);
            
            _playerWeaponSlots.RemoveAt(randomSlot);
        }

        public Weapon WeaponByString(string weaponName)
        {
            foreach (var weapon in allWeapons)
            {
                if (weapon.GetWeaponData().name == weaponName)
                {
                    return weapon;
                }
            }

            return null;
        }

        #endregion
        
    }
    
}
