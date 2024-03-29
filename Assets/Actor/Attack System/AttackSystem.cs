﻿using Assets.Common.ObjectPooling;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KS.Common.GameEvents;
using EventHandler = KS.Common.GameEvents.EventHandler;
using KS.Actor.Controllers;

namespace KS.Actor.Attack
{
    public class AttackSystem : MonoBehaviour
    {
        [SerializeField] private GameObject[] bulletPrefabs;
        [SerializeField] private float baseAttackRate;
        [SerializeField] private float attackRate;
        [SerializeField] private bool doubleGun;
        [SerializeField] private int activeBullet;

        // maybe you could create an array for these 
        private ObjectPool bulletPool;
        private ObjectPool bulletPool2;
        private ObjectPool bulletPool3;
        private bool isFiring = false;

        void Start()
        {
            activeBullet = 0;
            //Debug.Log("bulletPrefab.length: " + bulletPrefabs.Length);
            bulletPool = new ObjectPool(bulletPrefabs[activeBullet % 3]); // values that we can assign: 0, 1 and 2 
            bulletPool2 = new ObjectPool(bulletPrefabs[(activeBullet + 1) % 3]); // we have 3 bullet prefabs number should not be bigger than 2
            bulletPool3 = new ObjectPool(bulletPrefabs[(activeBullet + 2) % 3]);
        }

        public void Shoot()
        {
            if (!isFiring)
            {
                if (doubleGun)
                {
                    Quaternion rotation = Quaternion.identity;
                    rotation.eulerAngles = new Vector3(0f, 0f, 30f);

                    bulletPool2.CreateAtPosition(this.gameObject.transform.position + new Vector3(-0.5f, 0.5f, 0f), rotation);

                    bulletPool.CreateAtPosition(this.gameObject.transform.position, Quaternion.identity);

                    rotation.eulerAngles *= -1;
                    bulletPool3.CreateAtPosition(this.gameObject.transform.position + new Vector3(0.5f, 0.5f, 0f), rotation);
                }
                else
                {
                    bulletPool.CreateAtPosition(this.gameObject.transform.position, Quaternion.identity);
                }

                StartCoroutine(Delay(1 / attackRate));
                isFiring = true;
                EventHandler.instance.PublishGameEvent(GameEventType.OnBulletShotChanged, new string[0]);
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