using KS.Actor.Attack;
using KS.Actor.Health;
using KS.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public enum Team
    {
        Player,
        Enemy
    }

    public Team team;

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Carptı");

        // this part is conditions for to get damage
        HealthSystem collidingHealth = collider.gameObject.GetComponent<HealthSystem>();
        if (collidingHealth == null) // in our game this will be health of the asteroid that collides player if it's null break the func
        {
            return;
        }
        HealthSystem health = this.GetComponent<HealthSystem>(); // this means player here
        if (health == null)
        {
            return;
        }
        Actor other = collider.gameObject.GetComponent<Actor>(); // this is asteroid
        if (other == null)
        {
            return;
        }

        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (health.CanBeDamaged && damageDealer != null)
        {
            if (other.team == this.team)
            {
                return;
            }
            // if these conditions above provided , we can give damage to our player and the asteroid that collides the player will be gone by giving it maximum damage
            health.GetHit(damageDealer.Damage);
            collidingHealth.GetHit(float.MaxValue); // that means the object who collides will die and deal damage to player if player has not enough hp , player also die
        }

        if (collidingHealth.CanBeCollected)
        {
            Perk perk = other.gameObject.GetComponent<Perk>();
            if (perk == null)
            {
                return;
            }
            if (other.team != this.team)
            {
                return;
            }
            AttackSystem attackSystem = this.GetComponent<AttackSystem>(); // this means player here
            if (attackSystem == null)
            {
                return;
            }
            Perk perk1 = new Perk(5f, 2f, false);
            Perk perk2 = new Perk(5f, 1f, true);
            if (perk.DoubleGunActive == false)
            {
                attackSystem.AddPerk(perk1);
            }
            else
            {
                attackSystem.AddPerk(perk2);
            }
            collidingHealth.GetHit(float.MaxValue);
        }

    }

}
