using System;
using Obstacles;
using Obstacles.Door;
using Obstacles.Enemy;
using UnityEngine;

namespace Weapons.Projectile
{
    public class ProjectileCollider : MonoBehaviour
    {
        private Projectile _projectile;

        private void Awake()
        {
            _projectile = GetComponentInParent<Projectile>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

            switch (damageable)
            {
                case null:
                    Debug.LogWarning($"There is no type of {damageable}");
                    return;
                case Door door when door.CheckLockCondition():
                    damageable.Damage(1);
                    break;
                default:
                    damageable.Damage(_projectile.ProjectileDamage().Damage);
                    break;
            }

            Destroy(this.gameObject);

        }
    }
}
