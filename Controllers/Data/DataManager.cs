using System;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers.Data
{
    public class DataManager : MonoSingletonPersistent<DataManager>
    {
        private PlayerDataState _playerDataState;
            
        public Dictionary<PlayerDataType, Func<int>> CurrencyProperties { get; private set; }
        
        public static event Action<PlayerDataType> OnDataChanged;
    
        private void Awake()
        {
            _playerDataState= SaveManager.LoadData<PlayerDataState>("SavePlayerDataState");
            
            //First Time App Launching
            Application.targetFrameRate = 60;
        }
    
        #region GetPlayerStates
    
        public int Level
        {
            get => _playerDataState.level;
            set => SetLevel(value);
        }

        public int LevelIndex
        {
            get => _playerDataState.levelIndex;
            set => SetLevelIndex(value);
        }
    
        public int Hose
        {
            get => _playerDataState.hose;
            set => SetHose(value);
        }
    
        public int Coin
        {
            get => _playerDataState.coin;
            set => SetCoin(value);
        }
    
        public int Damage
        {
            get => _playerDataState.damage;
            set => SetDamage(value);
        }

        public int FireRate
        {
            get => _playerDataState.fireRate;
            set => SetFireRate(value);
        }
    
        public int Income
        {
            get => _playerDataState.income;
            set => SetIncome(value);
        }
    
        #endregion

        #region Set Player States

        private void SetLevel(int value)
        {
            _playerDataState.level = value;
            SaveManager.SaveData(_playerDataState,"SavePlayerDataState");
            OnDataChanged?.Invoke(PlayerDataType.Level);
        }
    
        private void SetLevelIndex(int value)
        {
            _playerDataState.levelIndex = value;
            SaveManager.SaveData(_playerDataState,"SavePlayerDataState");
            OnDataChanged?.Invoke(PlayerDataType.LevelIndex);
        }
    
        private void SetHose(int value)
        {
            _playerDataState.hose = value;
            SaveManager.SaveData(_playerDataState,"SavePlayerDataState");
            OnDataChanged?.Invoke(PlayerDataType.Hose);
        }

        private void SetCoin(int value)
        {
            _playerDataState.coin = value;
            SaveManager.SaveData(_playerDataState,"SavePlayerDataState");
            OnDataChanged?.Invoke(PlayerDataType.Coin);
        }

        private void SetDamage(int value)
        {
            _playerDataState.damage = value;
            SaveManager.SaveData(_playerDataState,"SavePlayerDataState");
            OnDataChanged?.Invoke(PlayerDataType.Damage);
        }
    
        private void SetFireRate(int value)
        {
            _playerDataState.fireRate = value;
            SaveManager.SaveData(_playerDataState,"SavePlayerDataState");
            OnDataChanged?.Invoke(PlayerDataType.FireRate);
        }
    
        private void SetIncome(int value)
        {
            _playerDataState.income = value;
            SaveManager.SaveData(_playerDataState,"SavePlayerDataState");
            OnDataChanged?.Invoke(PlayerDataType.Income);
        }
    
        #endregion
    
        #region Dictionary Values
    
        public DataManager()
        {
            //Exchanger values
            CurrencyProperties = new Dictionary<PlayerDataType, Func<int>>()
            {
                {PlayerDataType.Coin, () => Coin},
                {PlayerDataType.Hose, () => Hose},
                {PlayerDataType.Damage, () => Damage},
                {PlayerDataType.FireRate, () => FireRate},
                {PlayerDataType.Income, () => Income}
            };
        }
    
        #endregion

    }
}
