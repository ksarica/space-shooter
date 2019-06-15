using Assets.Common.ObjectPooling;
using KS.Actor.Health;
using KS.Actor.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KS.Actor.Controllers
{
    [RequireComponent(typeof(HealthSystem))]
    [RequireComponent(typeof(MovementSystem))]
    public class AIController : MonoBehaviour, IPoolableObject
    {
        [SerializeField] private Vector3 direction;
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
            float diffBefore = deathLineY - this.transform.position.y;
            this.movementSystem.Move(direction);
            float diffAfter = deathLineY - this.transform.position.y;
            if(diffAfter * diffBefore < 0)
            {
                healthSystem.GetHit(float.MaxValue);
            }
        }

        public void ResetPoolObject(Vector3 position)
        {
            healthSystem.ResetHitPoint();
            this.gameObject.transform.position = position;
            this.gameObject.SetActive(true);
        }
    }
}