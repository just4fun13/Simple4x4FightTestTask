using DG.Tweening;
using UnityEngine;


namespace Game
{
    public class SimpleBar : MonoBehaviour
    {
        private int _currentValue = 100;

        public int Value
        {
            get { return _currentValue; }
            set { 
                _currentValue = value;
                UpdateScale();
            }
        }

        private void Start()
        {
            transform.localScale = Vector3.one;
        }

        public void DealDamage(int damage)
        {
            Value -= damage;
        }

        private void UpdateScale() => transform.localScale = new Vector3(_currentValue*1f/100f, 1f, 1f);


    }
}
