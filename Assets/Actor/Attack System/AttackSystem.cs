using Assets.Common.ObjectPooling;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventHandler = KS.Common.EventHandler;

namespace KS.Actor.Attack
{
    public class AttackSystem : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float baseAttackRate;
        [SerializeField] private float attackRate;
        [SerializeField] private bool doubleGun;

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
                if (doubleGun)
                {
                    Quaternion rotation = Quaternion.identity;
                    rotation.eulerAngles = new Vector3(0f, 0f, 45f);
                    bulletPool.CreateAtPosition(this.gameObject.transform.position + new Vector3(-1f, 0.5f, 0f), rotation);
                    bulletPool.CreateAtPosition(this.gameObject.transform.position, Quaternion.identity);
                    rotation.eulerAngles *= -1;
                    bulletPool.CreateAtPosition(this.gameObject.transform.position + new Vector3(1f, 0.5f, 0f), rotation);
                }
                else
                {
                    bulletPool.CreateAtPosition(this.gameObject.transform.position, Quaternion.identity);
                }

                StartCoroutine(Delay(1 / attackRate));
                isFiring = true;
                EventHandler.instance.FireOnShotEvent(); // STEP 3 
            }
        }

        public IEnumerator Delay(float delayInSeconds)
        {
            yield return new WaitForSeconds(delayInSeconds);
            isFiring = false;
            yield return null;
        }

        public IEnumerator ResetAttackRate(float delayInSeconds)
        {
            yield return new WaitForSeconds(delayInSeconds);
            this.attackRate = this.baseAttackRate;
            yield return null;
        }

        public IEnumerator ResetGunOption(float delayInSeconds)
        {
            yield return new WaitForSeconds(delayInSeconds);
            this.doubleGun = false;
            yield return null;
        }

        Coroutine perkRoutine = null;
        public void AddPerk(Perk perk)
        {
            if (perk.DoubleGunActive == false)
            {
                this.attackRate = this.baseAttackRate * perk.BonusAttackRate;
                this.doubleGun = false;
                if (perkRoutine != null)
                {
                    StopCoroutine(perkRoutine);
                }
                perkRoutine = StartCoroutine(ResetAttackRate(perk.Duration));
            }
            else
            {
                this.attackRate = this.baseAttackRate;
                this.doubleGun = true;
                if (perkRoutine != null)
                {
                    StopCoroutine(perkRoutine);
                }
                perkRoutine = StartCoroutine(ResetGunOption(perk.Duration));
            }
        }
    }
}