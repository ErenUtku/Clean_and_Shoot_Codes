using Controllers.Data;
using PaintIn3D;
using UnityEngine;

namespace Player
{
    public class PlayerHoseController : MonoBehaviour
    {
        private P3dPaintSphere paintTool;
        private void Awake()
        {
            DataManager.OnDataChanged += ChangeHoseSize;

            paintTool = GetComponent<P3dPaintSphere>();
        }

        private void Start()
        {
            ChangeHoseSize(PlayerDataType.Hose);
        }

        private void OnDestroy()
        {
            DataManager.OnDataChanged -= ChangeHoseSize;
        }

        private void ChangeHoseSize(PlayerDataType dataType)
        {
            if (dataType == PlayerDataType.Hose)
            {
                float hoseLevel = DataManager.Instance.Hose -1; //Level starts with 1
                paintTool.Scale = new Vector3((hoseLevel / 10) + 1, paintTool.Scale.y, paintTool.Scale.z);
            }
        }
    }
}
