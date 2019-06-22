using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KS.Actor.Movement;
using KS.Actor.Attack;
using UnityEngine.SceneManagement;
using KS.Actor.Health;
using KS.Common;

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
            EventHandler.instance.OnDeath += OnPlayerDeath;
            GameUIController.Instance.SetScore(0);
            EndGameController.Instance.SetShotsFired(0);
            // health sisteminin OnDeath eventine OnPlayerDeath Fonksiyonunu register et.
            // healthSystem
        }

        // Update is called once per frame
        void Update()
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
            if (Input.GetKey(KeyCode.Space))
            {
                attackSystem.Shoot();
            }
        }

        void OnEnable()
        {
            Debug.Log("Enabled");
        }

        public void OnPlayerDeath(GameObject go)
        {
            if (go == this.gameObject)
            {
                SceneManager.LoadScene("EndGameMenu");
            }
        }
    }

}
