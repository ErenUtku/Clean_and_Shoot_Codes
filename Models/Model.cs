using UnityEngine;

namespace Models
{
    public class Model : MonoBehaviour
    {
        [SerializeField] private ModelData modelData;

        public ModelData GetModelData()
        {
            return modelData;
        }
        
    }
}
