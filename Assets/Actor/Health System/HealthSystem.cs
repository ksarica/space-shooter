using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KS.Actor.Health
{
    public class HealthSystem : MonoBehaviour
    {
        [SerializeField] private float maxHitPoint;
        [SerializeField] private bool canBeDamaged;
        public bool CanBeDamaged { get { return this.canBeDamaged; } }

        private float hitPoint;

        void Start()
        {
            ResetHitPoint();   
        }

        public void ResetHitPoint()
        {
            hitPoint = maxHitPoint;
        }

        public void GetHit(float damage) {
            hitPoint -= damage;
            if(hitPoint <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            this.gameObject.SetActive(false);
        }
    }
}
