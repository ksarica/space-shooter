using KS.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KS.Actor.Movement
{
    public class MovementSystem : MonoBehaviour
    {
        [SerializeField] private Vector3 boundaryMin; // game edges
        [SerializeField] private Vector3 boundaryMax; // game edges

        [SerializeField] private float speed;

        [SerializeField] private float minSpeed;
        [SerializeField] private float maxSpeed;

        public float MinSpeed { get => minSpeed; set => minSpeed = value; }
        public float MaxSpeed { get => maxSpeed; set => maxSpeed = value; }

        private void Start()
        {
            if (this.gameObject.GetComponent<Asteroid>() != null) // only asteroids will get random speed 
            {
                //minSpeed = 4;
                //maxSpeed = 8;
                SetSpeed(1f);
            }
        }

        public void Move(Vector3 displacement)
        {
            Vector3 positionAfter = this.gameObject.transform.position + (displacement * Time.deltaTime * speed); // check if the next position is inside of our game boundary if not assign the next position to maxboundaries
            positionAfter.x = Mathf.Clamp(positionAfter.x, boundaryMin.x, boundaryMax.x);
            positionAfter.y = Mathf.Clamp(positionAfter.y, boundaryMin.y, boundaryMax.y);
            this.gameObject.transform.position = positionAfter;
        }

        public void SetSpeed(float ratio)
        {
            minSpeed *= ratio;
            maxSpeed *= ratio;

            minSpeed = Mathf.Min(minSpeed, 12f);
            maxSpeed = Mathf.Min(maxSpeed, 25f);

            speed = Random.Range(minSpeed, maxSpeed);
        }
    }

}
