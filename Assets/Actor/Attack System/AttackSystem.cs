using Assets.Common.ObjectPooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KS.Actor.Attack
{
    public class AttackSystem : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float attackRate;
        private bool isFiring = false;
        private ObjectPool bulletPool;

        void Start()
        {
            bulletPool = new ObjectPool(bulletPrefab);
        }

        public void Shoot()
        {
            if (!isFiring)
            {
                bulletPool.CreateAtPosition(this.gameObject.transform.position);
                StartCoroutine(Delay(1 / attackRate));
                isFiring = true;
            }
        }

        public IEnumerator Delay(float delayInSeconds)
        {
            yield return new WaitForSeconds(delayInSeconds);
            isFiring = false;
            yield return null;
        }

    }
}