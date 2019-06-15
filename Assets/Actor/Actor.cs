using KS.Actor.Health;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public enum Team{
        Player,
        Enemy
    }

    public Team team;


    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Carptı");
        HealthSystem collidingHealth = collider.gameObject.GetComponent<HealthSystem>();
        if (collidingHealth == null)
        {
            return;
        }
        HealthSystem health = this.GetComponent<HealthSystem>();
        if (health == null)
        {
            return;
        }
        if (!health.CanBeDamaged)
        {
            return;
        }
        Actor other = collider.gameObject.GetComponent<Actor>();
        if (other == null)
        {
            return;
        }
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (damageDealer == null)
        {
            return;
        }
        if (other.team == this.team)
        {
            return;
        }
        health.GetHit(damageDealer.Damage);
        collidingHealth.GetHit(float.MaxValue);
    }

}
