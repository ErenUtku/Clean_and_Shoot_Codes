using System;
using Controllers.Data;
using Obstacles;
using UnityEngine;

namespace Utils
{
    public class Exchanger
    {
        public delegate void FailedExchange();

        public delegate void SuccessfulExchange();

        public void BuyCustomItem(PlayerDataType rewardType, PlayerDataType currencyType, int rewardAmount, int cost,
            SuccessfulExchange success_exchange, FailedExchange fail_exchange)
        {
            if (CheckCurrencyEnough(cost, currencyType))
            {
                AddCurrency(rewardAmount, rewardType);
                RemoveCurrency(cost, PlayerDataType.Coin);
                success_exchange();
            }
            else
            {
                Debug.LogWarning("Not enough currency to buy!");
                fail_exchange();
            }
        }

        public void AddCurrency(int amount, PlayerDataType currencyType)
        {
            if (DataManager.Instance.CurrencyProperties.TryGetValue(currencyType, out Func<int> getProperty))
            {
                int currentValue = getProperty();
                int newValue = currentValue + amount;
                SetProperty(currencyType, newValue);
            }
            else
            {
                Debug.LogWarning(
                    $"There is no such a currency type name called !! {currencyType} !! in AddCurrency Method");
            }
        }

        public void RemoveCurrency(int cost, PlayerDataType currencyType)
        {
            if (DataManager.Instance.CurrencyProperties.TryGetValue(currencyType, out Func<int> getProperty))
            {
                int currentValue = getProperty();
                int newValue = currentValue - cost;
                SetProperty(currencyType, newValue);
            }
            else
            {
                Debug.LogWarning(
                    $"There is no such a currency type name called !! {currencyType} !! in RemoveCurrency Method");
            }
        }
        
        public bool CheckCurrencyEnough(int cost, PlayerDataType currencyType)
        {
            int currencyAmount = GetCurrencyAmount(currencyType);
            if (currencyAmount >= cost)
            {
                return true;
            }
            else
            {
                Debug.LogWarning($"Not enough {currencyType} to complete the transaction.");
                return false;
            }
        }

        private int GetCurrencyAmount(PlayerDataType currencyType)
        {
            switch (currencyType)
            {
                case PlayerDataType.Coin:
                    return DataManager.Instance.Coin;
                default:
                    Debug.LogWarning(
                        $"There is no such a currency type name called !! {currencyType} !! in CheckCurrencyEnough Method");
                    return 0;
            }
        }

        private void SetProperty(PlayerDataType currencyType, int value)
        {
            switch (currencyType)
            {
                case PlayerDataType.Coin:
                    DataManager.Instance.Coin = value;
                    break;
                case PlayerDataType.Hose:
                    DataManager.Instance.Hose = value;
                    break;
                case PlayerDataType.Damage:
                    DataManager.Instance.Damage = value;
                    break;
                case PlayerDataType.FireRate:
                    DataManager.Instance.FireRate = value;
                    break;
                case PlayerDataType.Income:
                    DataManager.Instance.Income = value;
                    break;
                default:
                    Debug.LogWarning($"Purchasable property couldn't find {currencyType}");
                    break;
            }
        }
    }
}