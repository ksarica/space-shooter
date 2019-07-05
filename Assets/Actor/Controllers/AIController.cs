using Assets.Common.ObjectPooling;
using KS.Actor.Health;
using KS.Actor.Movement;
using KS.Common.GameEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KS.Actor.Controllers
{
    [RequireComponent(typeof(HealthSystem))]
    [RequireComponent(typeof(MovementSystem))]
    public class AIController : MonoBehaviour, IPoolableObject
    {
        public bool changeDirection = false;
        private bool directionChanged = false;
        public Vector3 direction;
        [SerializeField] private float deathLineY;
        MovementSystem movementSystem;
        HealthSystem healthSystem;

        // Start is called before the first frame update
        void Start()
        {
            direction.Normalize();
            movementSystem = this.GetComponent<MovementSystem>();
            healthSystem = this.GetComponent<HealthSystem>();
        }

        // Update is called once per frame
        void Update()
        {
            // below code is for if the bullet or asteroid , if they pass the deathLine , they will be get hit by maxValue .
            float diffBefore = deathLineY - this.transform.position.y; // before: (1,-2) diffBefore: -3 - (-2) = -1

            if (changeDirection && this.GetComponent<Asteroid>() != null)
            {
                //Debug.Log(this.name);
                if (!directionChanged)
                {
                    direction.x = Random.Range(-0.1f, 0.1f);
                    directionChanged = true;
                }
            }

            this.movementSystem.Move(direction);

            float diffAfter = deathLineY - this.transform.position.y; // after: (1,-3) diffAfter: -3 - (-3) = 0
            if (diffAfter * diffBefore < 0) // -1 * 0 = 0: false
            {
                healthSystem.GetHit(float.MaxValue);
                if (this.GetComponent<Asteroid>() != null)
                {
                    EventHandler.instance.PublishGameEvent(GameEventType.OnAsteroidDestroyed, new string[] { "1" });
                }
            }
        }

        public void ResetPoolObject(Vector3 position)
        {
            healthSystem.ResetHitPoint();
            this.gameObject.transform.position = position;
            this.gameObject.SetActive(true);
            ActorSoundPlayer actorSoundPlayer = this.GetComponent<ActorSoundPlayer>();
            if (actorSoundPlayer != null)
            {
                actorSoundPlayer.PlayOnStart();
            }
        }
    }
}