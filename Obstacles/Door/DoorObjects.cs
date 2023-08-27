using UnityEngine;

namespace Obstacles.Door
{
    public class DoorObjects : DamageableObjects
    {
        public void DoorLockActivation(bool value)
        {
            mainObject.SetActive(value);
        }
    }
}
