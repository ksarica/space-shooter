using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perk : MonoBehaviour
{
    [SerializeField] private float duration;
    public float Duration { get => duration; }

    [SerializeField] private float bonusAttackRate;
    public float BonusAttackRate { get => bonusAttackRate; }

    [SerializeField] private bool doubleGunActive;
    public bool DoubleGunActive { get => doubleGunActive; }

    public Perk(float dur,float rate,bool doubleGun)
    {
        this.duration = dur;
        this.bonusAttackRate = rate;
        this.doubleGunActive = doubleGun;
    }
}
