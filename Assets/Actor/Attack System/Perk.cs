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

    
}
