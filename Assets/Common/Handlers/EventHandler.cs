using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KS.Common
{
    public class EventHandler : MonoBehaviour
    {
        public delegate void OnCoreGameEvent(string eventName);
        public event OnCoreGameEvent CoreGameEvents;

        public delegate void OnDeathOccurred(GameObject go);
        public event OnDeathOccurred OnDeath;

        public delegate void OnShotFired(); // STEP 1 
        public event OnShotFired OnShoot; // STEP 1

        public delegate void OnHitOccurred(GameObject go); // STEP 1
        public event OnHitOccurred OnHit; // STEP 1


        public static EventHandler instance = null;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;

            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            //coreGameEvents += OnGameEventFired;
        }


        // Update is called once per frame
        void Update()
        {

        }

        public void FireCoreGameEvent(string eventName)
        {
            if (CoreGameEvents != null && CoreGameEvents.GetInvocationList().Length > 0)
            {
                CoreGameEvents(eventName);
            }

        }

        public void FireOnDeathEvent(GameObject go)
        {
            if (OnDeath != null && OnDeath.GetInvocationList().Length > 0)
            {
                OnDeath(go);
            }
        }

        public void FireOnShotEvent() // STEP 2
        {
            if (OnShoot != null && OnShoot.GetInvocationList().Length > 0)
            {
                OnShoot();
            }
        }

        public void FireOnHitEvent(GameObject go)
        {
            if (OnHit != null && OnHit.GetInvocationList().Length > 0)
            {
                OnHit(go);
            }
        }
    }
}
