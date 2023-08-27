using UnityEngine;

namespace Player
{
    public abstract class AbstractPlayerMoveController : MonoBehaviour
    {
        #region SERIALIZE FIELDS

        [Header("Movement")]
        public float forwardSpeed;
        [Range(1,10)]
        public float speedMultiplier;
        public float horizontalSpeed;
        public float maximumHorizontalPosition;

        [Header("Player")]
        public GameObject player;
        
        #endregion

        #region ABSTRACT METHODS

        public abstract void HorizontalMoveControl();
        public abstract void StartRun();
        public abstract void StopRun();
        public abstract void StopHorizontalControl(bool controlIsLock = true);

        #endregion
        
        #region PROTECTED VIRTUAL METHODS

        protected Vector3 HorizontalPosition(Vector3 targetPosition, float swipeDelta)
        {
            var xDirection = Time.deltaTime * swipeDelta * horizontalSpeed;
            var position = targetPosition;
            var xPos = Mathf.Clamp(
                position.x + xDirection,
                maximumHorizontalPosition * -1,
                maximumHorizontalPosition);

            position = new Vector3(xPos, position.y, position.z);

            return position;
        }

        protected virtual void OnComponentAwake()
        {
            
        }

        protected virtual void OnComponentStart()
        {
            
        }

        protected virtual void OnComponentUpdate()
        {
            
        }

        protected virtual void OnComponentDestroy()
        {
            
        }

        #endregion

        #region UNITY EVENTS

        private void Awake() => OnComponentAwake();
        private void Start() => OnComponentStart();
        private void Update() => OnComponentUpdate();
        private void OnDestroy() => OnComponentDestroy();

        #endregion
    }
}