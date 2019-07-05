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

    [SerializeField] private float repairPoint;
    public float RepairPoint { get => repairPoint; }

}
