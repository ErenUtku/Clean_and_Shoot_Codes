using System;
using Controllers.Data;
using Controllers.State;
using UnityEditor;
using UnityEngine;

namespace Weapons
{
    [RequireComponent(typeof(FireControl),typeof(CleanControl),typeof(MovementControl))]
    [RequireComponent(typeof(AnimatorControl))]
    public class Weapon : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private WeaponData weaponData;
        [Header("Positions")]
        [SerializeField] private Transform projectileInitTransform;
        [Header("Objects")]
        [SerializeField] private GameObject rustedSurfaceGameObject;      
        public FireControl FireControl { get; private set; }
        public CleanControl RustControl { get; private set; }   
        public MovementControl MovementControl { get; private set; }
        
        public AnimatorControl AnimatorControl { get; private set; }

        private bool _collectedByPlayer;

        private void Awake()
        {
            MovementControl = GetComponent<MovementControl>();
            FireControl = GetComponent<FireControl>();  
            RustControl = GetComponent<CleanControl>();
            AnimatorControl = GetComponent<AnimatorControl>();

            DataManager.OnDataChanged += WeaponLevelActivation;

            #region States
            
            FightState.FightStateActive += WeaponFieryActivation;
            ScoreState.ScoreStateActive += WeaponFieryActivation;
            
            #endregion
        }

        private void Start()
        {
            WeaponLevelActivation(PlayerDataType.Hose);
        }

        private void OnDestroy()
        {
            DataManager.OnDataChanged -= WeaponLevelActivation;
            FightState.FightStateActive -= WeaponFieryActivation;
            ScoreState.ScoreStateActive -= WeaponFieryActivation;
        }

        #region Public Fields

        //Starting Getters 
        public WeaponData GetWeaponData()
        {
            return weaponData;
        }

        public Transform GetProjectileInitTransform()
        {
            return projectileInitTransform;
        }

        public GameObject GetRustedSurfaceGameObject()
        {
            return rustedSurfaceGameObject;
        }
        
        //Ending Getters  
        
        public bool IsCollected()
        {
            if (RustControl.WeaponBrushed())
                _collectedByPlayer = true;
            
            return _collectedByPlayer;
        }

        #endregion

        #region Private Fields

        private void WeaponLevelActivation(PlayerDataType dataType)
        {
            if (dataType != PlayerDataType.Hose) return;
            
            var hoseLevel = DataManager.Instance.Hose;
            bool isLevelSufficient = hoseLevel >= weaponData.level;

            RustControl.PaintActivation(isLevelSufficient);

            rustedSurfaceGameObject.GetComponent<MeshCollider>().enabled = isLevelSufficient;
        }

        private void WeaponFieryActivation(bool value)
        {
            FireControl.ActivateFiery(value);
        }

        #endregion
    }
}
