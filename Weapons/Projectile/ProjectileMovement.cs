using UnityEngine;

namespace Weapons.Projectile
{
    public class ProjectileMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
        private float _destroyDelay;
    
        private void Start()
        {
            DestroyCalculation();
        }

        private void Update()
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            
            Destroy(gameObject, _destroyDelay);
        }

        private void DestroyCalculation()
        {
            _destroyDelay = 20f / speed;
        }
    }
}
