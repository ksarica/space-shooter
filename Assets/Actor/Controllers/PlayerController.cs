using UnityEngine;
using KS.Actor.Movement;
using KS.Actor.Attack;
using UnityEngine.SceneManagement;
using KS.Actor.Health;
using KS.Common.GameEvents;
using System;
using EventHandler = KS.Common.GameEvents.EventHandler;
using Assets.Common;

namespace KS.Actor.Controllers
{
    [RequireComponent(typeof(AttackSystem))]
    [RequireComponent(typeof(MovementSystem))]
    [RequireComponent(typeof(HealthSystem))]
    public class PlayerController : MonoBehaviour
    {
        AttackSystem attackSystem;
        MovementSystem movementSystem;
        HealthSystem healthSystem;

        // Start is called before the first frame update
        void Start()
        {
            healthSystem = this.GetComponent<HealthSystem>();
            attackSystem = this.GetComponent<AttackSystem>();
            movementSystem = this.GetComponent<MovementSystem>();
            EventHandler.instance.Subscribe(GameEventType.OnPlayerDeath, PlayerDeath);
            EventHandler.instance.PublishGameEvent(GameEventType.OnPlayerHealthChanged, new string[] { healthSystem.HitPoint.ToString() });
        }

        private void OnDestroy()
        {
            EventHandler.instance.Unsubscribe(GameEventType.OnPlayerDeath, PlayerDeath);
        }

        private void PlayerDeath(string[] eventData)
        {
            SceneManager.LoadScene("EndGameMenu");
        }

        // Update is called once per frame
        void Update()
        {
            if (KSInput.GetPlatformType() == KSInput.PlatformType.PC)
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    movementSystem.Move(Vector3.left);
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    movementSystem.Move(Vector3.right);
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    movementSystem.Move(Vector3.down);
                }
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    movementSystem.Move(Vector3.up);
                }
            }

            if (KSInput.HasInput())
            {
                KSInput.SetStartPosition();
                movementSystem.Move(KSInput.GetDeltaPosition());
            }

            attackSystem.Shoot();
        }



    }

}
