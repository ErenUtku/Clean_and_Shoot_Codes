using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Controllers.State;
using Levels;
using MoreMountains.Tools;
using Obstacles;
using Obstacles.Door;
using Player;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(MMPathMovement))]
    public class PlayerMovement : AbstractPlayerMoveController
    {
        [Header("Knock Details")]
        [Range(1,5)]
        [SerializeField] private float knockAmount;
        [Range(1,5)]
        [SerializeField] private float knockTime;
            
        #region PRIVATE FIELDS

        private bool _isMove;
        private MMPathMovement _mmPathMovement;
        private bool _isHorizontalMoveLock;
        private float _mouseXStartPosition;
        private float _swipeDelta;
        private float _defaultSpeed;
        
        #endregion
        
        #region PRIVATE METHODS

        private void SetPath(Level levelData)
        {
            //ToDo Path seems like always straight, but if not, get it from level data
        }

        private void SpeedBoost(bool value)
        {
            forwardSpeed = _defaultSpeed * (value ? speedMultiplier : speedMultiplier / 2);
            
            _mmPathMovement.MovementSpeed = forwardSpeed;
        }

        private void KnockPlayerBack()
        {
            StartCoroutine(KnockPlayerCoroutine());
        }

        IEnumerator KnockPlayerCoroutine()
        {
            var defaultSpeed = forwardSpeed;
            
            forwardSpeed = -knockAmount;
            
            _mmPathMovement.MovementSpeed = forwardSpeed;
            
            yield return new WaitForSeconds(knockTime);
            
            forwardSpeed = defaultSpeed;
            
            _mmPathMovement.MovementSpeed = forwardSpeed;
        }

        #endregion
        
        #region PUBLIC METHODS

        public override void StartRun()
        {
            _isMove = true;
            
            SpeedBoost(false);
            
            _mmPathMovement.MovementSpeed = forwardSpeed;
        }
        public override void StopRun()
        {
            _isMove = false;
            _mmPathMovement.MovementSpeed = 0;
        }
        public override void StopHorizontalControl(bool controlIsLock = true) => _isHorizontalMoveLock = controlIsLock;
        public override void HorizontalMoveControl()
        {
            if (_isHorizontalMoveLock) return;

            // MOUSE DOWN
            if (Input.GetMouseButtonDown(0))
            {
                _mouseXStartPosition = Input.mousePosition.x;
            }

            // MOUSE ON PRESS
            if (Input.GetMouseButton(0))
            {
                _swipeDelta = Input.mousePosition.x - _mouseXStartPosition;
                _mouseXStartPosition = Input.mousePosition.x;

                // Horizontal Move
                var playerPosition = HorizontalPosition(player.transform.localPosition, _swipeDelta);
                player.transform.localPosition = playerPosition;
                
                return;
            }

            // MOUSE UP
            if (Input.GetMouseButtonUp(0)) _swipeDelta = 0;

        }
        
        #endregion
        
        #region CUSTOM EVENT METHODS

        private void OnLevelLoad(Level levelData) => SetPath(levelData);

        private void OnLevelStart(Level levelData) => StartRun();

        private void OnLevelFail(Level levelData)
        {
            StopRun();
            StopHorizontalControl();
        }

        private void OnLevelStageComplete(Level levelData, int stageIndex)
        {
            StopHorizontalControl();
        }

        private void OnLevelComplete(Level levelData)
        {
            StopRun();
            StopHorizontalControl();
        }

        #endregion
        
        #region UNITY EVENT METHODS

        protected override void OnComponentAwake()
        {
            base.OnComponentAwake();
            _mmPathMovement = this.gameObject.GetComponent<MMPathMovement>();
            _defaultSpeed = forwardSpeed;

            FightState.FightStateActive += SpeedBoost;
            DamageableTypes.KnockPlayer += KnockPlayerBack;
        }
        
        protected override void OnComponentStart()
        {
            base.OnComponentStart();
            LevelManager.OnLevelStart += OnLevelStart;
            LevelManager.OnLevelStageComplete += OnLevelStageComplete;
            LevelManager.OnLevelFail += OnLevelFail;
            LevelManager.OnLevelComplete += OnLevelComplete;
            
            
            _mmPathMovement.MovementSpeed = 0;
        }
        
        protected override void OnComponentUpdate()
        {
            if (!_isMove) return;

            HorizontalMoveControl();
        }

        protected override void OnComponentDestroy()
        {
            LevelManager.OnLevelLoad -= OnLevelLoad;
            LevelManager.OnLevelStart -= OnLevelStart;
            LevelManager.OnLevelStageComplete -= OnLevelStageComplete;
            LevelManager.OnLevelFail -= OnLevelFail;
            LevelManager.OnLevelComplete -= OnLevelComplete;
            
            FightState.FightStateActive -= SpeedBoost;
            DamageableTypes.KnockPlayer -= KnockPlayerBack;
        }

        #endregion
        
    }
}

