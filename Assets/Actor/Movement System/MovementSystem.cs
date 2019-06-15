using KS.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KS.Actor.Movement
{
    public class MovementSystem : MonoBehaviour
    {
        [SerializeField] private Vector3 boundaryMin;
        [SerializeField] private Vector3 boundaryMax;
        [SerializeField] private float speed;
        public void Move(Vector3 displacement)
        {
            Vector3 positionAfter = this.gameObject.transform.position + (displacement * Time.deltaTime * speed);
            positionAfter.x = Mathf.Clamp(positionAfter.x, boundaryMin.x, boundaryMax.x);
            positionAfter.y = Mathf.Clamp(positionAfter.y, boundaryMin.y, boundaryMax.y);
            this.gameObject.transform.position = positionAfter;
            EventHandler.instance.FireCoreGameEvent("moved");
        }
    }

}
