using UnityEngine;

namespace Animations
{
    public class RotateAnimation : MonoBehaviour
    {
        private float _rotationSpeed = 135f; // Adjust this value to control the rotation speed

        [SerializeField] private bool rotateX;
        [SerializeField] private bool rotateY;
        [SerializeField] private bool rotateZ;

        private void Update()
        {
            Vector3 rotation = Vector3.zero;
            
            if (rotateX)
            {
                rotation += Vector3.right;
            }

            if (rotateY)
            {
                rotation += Vector3.up;
            }

            if (rotateZ)
            {
                rotation += Vector3.forward;
            }

            transform.Rotate(rotation * _rotationSpeed * Time.deltaTime);
        }
    }
}
