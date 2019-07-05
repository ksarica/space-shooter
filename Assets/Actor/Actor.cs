using KS.Actor.Attack;
using KS.Actor.Controllers;
using KS.Actor.Health;
using KS.Common;
using KS.Common.GameEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public enum ActorType
    {
        Player,
        Asteroid,
        Bullet,
        AttackSpeedBonus,
        TripleGunBonus,
        RepairBonus,
        Gold
    }

    public ActorType actorType;

    private void OnTriggerEnter(Collider collider)
    {
        Actor subjectA = this;
        Actor subjectB = collider.gameObject.GetComponent<Actor>();

        if (subjectA.actorType == ActorType.Player) // subjectA = player ==> subjectB = other objects
        {
            switch (subjectB.actorType)
            {
                case ActorType.Asteroid:
                    subjectB.GetComponent<HealthSystem>().GetHit(float.MaxValue);
                    EventHandler.instance.PublishGameEvent(GameEventType.OnAsteroidDestroyed, new string[] { "0" });
                    subjectA.GetComponent<HealthSystem>().GetHit(subjectB.GetComponent<DamageDealer>().Damage);
                    EventHandler.instance.PublishGameEvent(GameEventType.OnPlayerHealthChanged, new string[] { subjectA.GetComponent<HealthSystem>().HitPoint.ToString() });
                    if (subjectA.GetComponent<HealthSystem>().HitPoint <= 0)
                    {
                        EventHandler.instance.PublishGameEvent(GameEventType.OnPlayerDeath, new string[0]);
                    }
                    break;

                case ActorType.AttackSpeedBonus:
                    subjectA.GetComponent<AttackSystem>().AddPerk(subjectB.GetComponent<Perk>());
                    subjectB.GetComponent<HealthSystem>().GetHit(float.MaxValue);
                    break;
                case ActorType.TripleGunBonus:
                    subjectA.GetComponent<AttackSystem>().AddPerk(subjectB.GetComponent<Perk>());
                    subjectB.GetComponent<HealthSystem>().GetHit(float.MaxValue);
                    break;
                case ActorType.RepairBonus:
                    subjectA.GetComponent<HealthSystem>().GetHit(-25); // -25 damage will repair 25 hitpoints actually
                    EventHandler.instance.PublishGameEvent(GameEventType.OnPlayerHealthChanged, new string[] { subjectA.GetComponent<HealthSystem>().HitPoint.ToString() });
                    subjectB.GetComponent<HealthSystem>().GetHit(float.MaxValue);
                    break;
                case ActorType.Gold:
                    EventHandler.instance.PublishGameEvent(GameEventType.OnGoldCollected, new string[0]);
                    subjectB.GetComponent<HealthSystem>().GetHit(float.MaxValue);
                    break;
            }
        }
        else if (subjectA.actorType == ActorType.Asteroid && subjectB != null)
        {
            switch (subjectB.actorType)
            {
                case ActorType.Bullet:
                    subjectB.GetComponent<HealthSystem>().GetHit(float.MaxValue);
                    subjectA.GetComponent<HealthSystem>().GetHit(subjectB.GetComponent<DamageDealer>().Damage);
                    EventHandler.instance.PublishGameEvent(GameEventType.OnAsteroidHit, new string[0]);
                    if (subjectA.GetComponent<HealthSystem>().HitPoint <= 0)
                    {
                        EventHandler.instance.PublishGameEvent(GameEventType.OnAsteroidDestroyed, new string[] { "combo" });
                    }
                    break;
            }
        }

    }

}
