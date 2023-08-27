using System.Collections;
using UnityEngine;

namespace Weapons
{
    public class MovementControl : MonoBehaviour
    {
        [SerializeField] private Quaternion targetRotation;
        public void MoveWeaponToPlayer(Transform targetPosition)
        {
            StartCoroutine(MoveWeaponCoroutine(targetPosition));
        }
        private IEnumerator MoveWeaponCoroutine(Transform targetPosition)
        {
            this.gameObject.transform.SetParent(targetPosition);  //Set Parent of player inorder to make weapon follow player
        
            //Ground to player movement transform values
            var duration = 1.5f; 
            var elapsedTime = 0f;
            
            //Transform Target position
            Vector3 initialPosition = transform.position;
            
            //Rotation => 0,0,0
            Quaternion initialRotation = transform.rotation;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / duration);
                transform.position = Vector3.Lerp(initialPosition, targetPosition.position, t);
                transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, t);
                yield return null;
            }

            //Ensuring
            transform.position = targetPosition.position;
            transform.rotation = targetRotation;

        }
    }
}
