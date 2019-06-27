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
    public enum Team
    {
        Player,
        Enemy
    }

    public Team team;

    public enum ActorType
    {
        Player,
        Asteroid,
        Bullet,
        AttackSpeedBonus,
        TripleGunBonus,
        Gold
    }

    public ActorType actorType;

    private void OnTriggerEnter(Collider collider)
    {
        Actor subjectA = this;
        Actor subjectB = collider.gameObject.GetComponent<Actor>();

        if (subjectA.actorType == ActorType.Player)
        {
            switch (subjectB.actorType)
            {
                case ActorType.Asteroid:
                    subjectB.GetComponent<HealthSystem>().GetHit(float.MaxValue);
                    EventHandler.instance.PublishGameEvent(GameEventType.OnAsteroidDestroyed, new string[0]);
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
                case ActorType.Gold:
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
                        EventHandler.instance.PublishGameEvent(GameEventType.OnAsteroidDestroyed, new string[0]);
                    }

                    break;

            }
        }

    }

}
