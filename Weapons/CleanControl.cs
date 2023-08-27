using PaintIn3D;
using UnityEngine;

namespace Weapons
{
    public class CleanControl : MonoBehaviour
    {
        private GameObject _rustedSurfaceGameObject;
        
        //Paint3D Data
        private P3dPaintable _p3dPaintable;
        private P3dChannelCounter _paintChangeCounter;

        #region UNNITY EVENS
        private void Awake()
        {
            _rustedSurfaceGameObject = GetComponent<Weapon>().GetRustedSurfaceGameObject();
            _p3dPaintable = _rustedSurfaceGameObject.GetComponent<P3dPaintable>();
            _paintChangeCounter = _rustedSurfaceGameObject.GetComponent<P3dChannelCounter>();
        }
        
        #endregion

        #region PUBLIC FIELDS

        public bool WeaponBrushed()
        {
            return _paintChangeCounter.CountA < _paintChangeCounter.Total / 2;
        }

        public void PaintActivation(bool value)
        {
            _p3dPaintable.enabled = value;
        }

        #endregion
    }
}
