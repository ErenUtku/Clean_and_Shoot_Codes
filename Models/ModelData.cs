using UnityEngine;

namespace Models
{
    [CreateAssetMenu(fileName = "ModelData", menuName = "GameData/ModelData", order = 1)]
    public class ModelData : ScriptableObject
    {
        public int minValue;
        public int maxValue;
    }
}
