using System;

namespace Controllers.Data
{
    [Serializable]
    public class PlayerDataState
    {
        public int level = 1; //Default value => 1
        public int levelIndex = 0; //Default Value => 0
        public int hose = 1; //Default value => 1
        public int coin = 0;  //Default value => 0
        public int damage = 1; //Default value => 1
        public int fireRate = 1; //Default value => 1
        public int income = 1; //Default value => 1
    }
}

public enum PlayerDataType
{
    Level,
    LevelIndex,
    Hose,
    Coin,
    Damage,
    FireRate,
    Income
}