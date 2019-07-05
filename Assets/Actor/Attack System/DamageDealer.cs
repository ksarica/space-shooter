using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public float Damage { get => damage;}
    [SerializeField] private float damage;
    [SerializeField] private float minDamage;
    [SerializeField] private float maxDamage;
    [SerializeField] private bool randomDamage = false;

    private void Start()
    {
        if (randomDamage)
        {
            damage = Mathf.Ceil(Random.Range(minDamage, maxDamage));
        }
    }

}
