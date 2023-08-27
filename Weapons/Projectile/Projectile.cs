using System;
using UnityEngine;

namespace Weapons.Projectile
{
    [RequireComponent(typeof(ProjectileMovement),typeof(ProjectileDamage),typeof(ProjectileCollider))]
    public class Projectile : MonoBehaviour
    {
        private ProjectileMovement _projectileMovement;
        private ProjectileDamage _projectileDamage;
        private ProjectileCollider _projectileCollider;

        #region Getters

        public ProjectileDamage ProjectileDamage()
        {
            return _projectileDamage;
        }

        #endregion

        #region UNITY EVENTS

        private void Awake()
        {
            _projectileMovement = GetComponent<ProjectileMovement>();
            _projectileDamage= GetComponent<ProjectileDamage>();
            _projectileCollider= GetComponent<ProjectileCollider>();
        }

        #endregion
    }
}