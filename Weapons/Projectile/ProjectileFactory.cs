using UnityEngine;

namespace Weapons.Projectile
{
    public class ProjectileFactory
    {
        public static Projectile CreateProjectile(WeaponData weaponData, float damage)
        {
            GameObject newProjectileObject = Object.Instantiate(weaponData.projectile);
            Projectile newProjectile = newProjectileObject.GetComponent<Projectile>();
            newProjectile.ProjectileDamage().Damage = damage;
            return newProjectile;
        }
    }
}