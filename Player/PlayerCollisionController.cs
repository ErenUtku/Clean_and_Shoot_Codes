using Controllers;
using Controllers.State;
using Currency;
using Obstacles;
using UnityEngine;
using Weapons;

namespace Player
{
    public class PlayerCollisionController : MonoBehaviour
    {
        #region PRIVATE FIELDS

        private SphereCollider _playerCollider;
        private BoxCollider _weaponDetectionCollider;
        
        #endregion

        #region PRIVATE METHODS

        #endregion

        #region UNITY EVENT METHODS

        private void Awake()
        {
            _playerCollider = GetComponent<SphereCollider>();
            _weaponDetectionCollider = GetComponent<BoxCollider>();

            PlayerColliderActivation(false);

            FightState.FightStateActive += PlayerColliderActivation;
            ScoreState.ScoreStateActive += PlayerColliderActivation;
        }

        private void OnDestroy()
        {
            FightState.FightStateActive -= PlayerColliderActivation;
            ScoreState.ScoreStateActive -= PlayerColliderActivation;
        }

        // TRIGGER EVENTS
        private void OnTriggerEnter(Collider other)
        {
            
            ICollectable damageable = other.gameObject.GetComponent<ICollectable>();

            damageable?.CollectCurrency();
            
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("WeaponPlate"))
            {
                var selectedObject = other.GetComponentInParent<WeaponPlate>().GetWeapon();
                var selectedWeapon = selectedObject.GetComponent<Weapon>();
                
                if (selectedWeapon.RustControl.WeaponBrushed())
                {
                    WeaponManager.Instance.AddWeaponToPlayer(selectedWeapon.GetComponent<Weapon>());
                    selectedWeapon.GetComponent<Weapon>().GetRustedSurfaceGameObject().SetActive(false);
                }
            }
        }


        // COLLISION EVENTS

        private void OnCollisionEnter(Collision collision)
        {
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

            damageable?.CollisionBehavior();
        }

        private void OnCollisionExit(Collision other)
        {
            
        }

        #endregion

        #region PRIVATE FIELDS

        private void PlayerColliderActivation(bool value)
        {
            _playerCollider.enabled = value;
            _weaponDetectionCollider.enabled = !value;
        }

        #endregion
    }
}