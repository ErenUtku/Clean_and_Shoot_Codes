using System;
using UnityEngine;

namespace Obstacles.Enemy
{
    public class EnemyMaterials : DamageableMaterial
    {
        [SerializeField] private GameObject maskedObject;
        [SerializeField] private float topYPos;
        [SerializeField] private float bottomYPos;
        private void Start()
        {
            //Set RenderQueue of main object
            mainRenderer.material.renderQueue = 3002;

            
        }

        public void UpdateMaskedObjectPosition(float health, float selectedHealth)
        {
            float healthPercentage = health / selectedHealth;
            float yPos = Mathf.Lerp(bottomYPos, topYPos, healthPercentage);
            maskedObject.transform.position = new Vector3(maskedObject.transform.position.x, yPos, maskedObject.transform.position.z);
        }
    }
}
