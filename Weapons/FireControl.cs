using Controllers.Data;
using Obstacles;
using Obstacles.Door;
using UnityEngine;
using Weapons.Projectile;


namespace Weapons
{
    public class FireControl : MonoBehaviour
    {
        private Weapon _weapon;
        
        //Weapon Data
        private WeaponData _weaponData;
        private float _damage;
        private float _fireRate;
        
        //Weapon Behavior
        private bool _activeFire;
        private float _nextFireTime;

        #region UNITYEVENTS

        private void Awake()
        {
            _weapon = GetComponent<Weapon>();
            _weaponData = _weapon.GetWeaponData();
            
            SetupWeaponArmory();

            Door.ApplyBuff += IncreaseWeaponPower;
            
        }

        private void OnDestroy()
        {
            Door.ApplyBuff -= IncreaseWeaponPower;
        }

        private void Update()
        {
            if (_activeFire && Time.time >= _nextFireTime)
            {
                FireProjectile();
                _nextFireTime = Time.time + 1f / _fireRate;
            }
        }

        #endregion

        #region PRIVATE FIELDS

        private void SetupWeaponArmory()
        {
            _damage = _weaponData.damage + DataManager.Instance.Damage;
            _fireRate = _weaponData.fireRate + (DataManager.Instance.FireRate/10);
            _activeFire = false;
        }

        private void FireProjectile()
        {
            Projectile.Projectile newProjectile = ProjectileFactory.CreateProjectile(_weaponData, _damage);
            
            newProjectile.transform.position = _weapon.GetProjectileInitTransform().position;
            
            //Animation
            _weapon.AnimatorControl.FireAnimation();
        }
        
        #endregion


        #region PUBLIC FIELDS
        
        public void ActivateFiery(bool value)
        {
            if (!_weapon.IsCollected()) return;
            _activeFire = value;
        }

        private void IncreaseWeaponPower(float increment,BuffType buffType)
        {
            switch (buffType)
            {
                case BuffType.Damage:
                    
                    _damage += increment;
                    if (_damage < 0)
                    {
                        _damage = 0.1f;
                    }
                    
                    break;
                case BuffType.FireRate:
                    
                    _fireRate += increment/10; //fireRate will be so powerful
            
                    if (_fireRate < 0)
                    {
                        _fireRate = 0.1f;
                    }
                    
                    break;
                
                default:
                    Debug.LogWarning("No such a buff found");
                    return;
            }
        }
        

        public void DisarmFiery()
        {
            _activeFire = false;
        }

        #endregion
        
        
    }
}
