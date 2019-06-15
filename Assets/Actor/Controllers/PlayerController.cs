using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KS.Actor.Movement;
using KS.Actor.Attack;

namespace KS.Actor.Controllers
{
    [RequireComponent(typeof(AttackSystem))]
    [RequireComponent(typeof(MovementSystem))]
    public class PlayerController : MonoBehaviour
    {
        AttackSystem attackSystem;
        MovementSystem movementSystem;
        // Start is called before the first frame update
        void Start()
        {
            attackSystem = this.GetComponent<AttackSystem>();
            movementSystem = this.GetComponent<MovementSystem>();
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
    }

}
