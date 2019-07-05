using KS.Common;
using KS.Common.GameEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KS.Actor.Health
{
    public class HealthSystem : MonoBehaviour
    {

        [SerializeField] private float maxHitPoint;
        [SerializeField] private bool canBeDamaged;
        [SerializeField] private bool canBeCollected;
        [SerializeField] private GameObject deathFX;
        [SerializeField] private GameObject deathSoundFX;

        public bool CanBeDamaged { get { return this.canBeDamaged; } }
        public bool CanBeCollected { get { return this.canBeCollected; } }

        public float HitPoint { get => hitPoint; set => hitPoint = value; }
        private float hitPoint;


        void Start()
        {
            ResetHitPoint();
        }

        public void ResetHitPoint()
        {
            hitPoint = maxHitPoint;
        }

        public void GetHit(float damage)
        {
            hitPoint -= damage;
            if (hitPoint > maxHitPoint)
            {
                hitPoint = maxHitPoint;
            }
            if (hitPoint <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            this.gameObject.SetActive(false);
            if (deathFX != null)
            {
                Instantiate(deathFX, this.transform.position, Quaternion.identity);
            }
            if (deathSoundFX != null)
            {
                Instantiate(deathSoundFX, this.transform.position, Quaternion.identity);
            }
        }
    }
}
