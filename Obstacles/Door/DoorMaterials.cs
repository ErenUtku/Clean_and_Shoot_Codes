using UnityEngine;

namespace Obstacles.Door
{
    public class DoorMaterials : DamageableMaterial
    {
        [SerializeField] private Material positiveMaterial;
        [SerializeField] private Material negativeMaterial;

        public void MaterialColorUpdate(float amount)
        {
            mainRenderer.material = amount < 0 ? negativeMaterial : positiveMaterial;
        }
    }
}
