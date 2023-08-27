
using UnityEngine;

namespace Obstacles.Door
{
    public class DoorMovement : MonoBehaviour
    {
        [SerializeField] private bool isMove;
        [SerializeField] private float maxDistance;
        [SerializeField] private float speed;

        private bool changeDirection = true;
        
        private void Update()
        {
            if (isMove)
            {
                float direction = changeDirection ? 1f : -1f;
                float distance = direction * speed * Time.deltaTime;

                transform.Translate(distance, 0f, 0f);

                changeDirection = changeDirection switch
                {
                    true when transform.position.x > maxDistance => false,
                    false when transform.position.x < -maxDistance => true,
                    _ => changeDirection
                };
            }
        }
    }
}
