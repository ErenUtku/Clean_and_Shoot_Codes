using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "GameData/WeaponData", order = 1)]
    public class WeaponData : ScriptableObject
    {
        public string name;
        public GameObject projectile;
        public int level;
        public float damage;
        public float fireRate;
    }
}

