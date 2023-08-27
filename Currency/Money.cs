using UnityEngine;
using Utils;

namespace Currency
{
    public class Money : MonoBehaviour,ICollectable
    {
        private Exchanger _exchanger;

        private void Awake()
        {
            _exchanger = new Exchanger();
        }

        public void CollectCurrency()
        {
            _exchanger.AddCurrency(50,PlayerDataType.Coin);
            
            Destroy(this.gameObject);
        }
    }
}
