using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace KS.Common
{
    public class EventHandler : MonoBehaviour
    {
        public delegate void OnCoreGameEvent(string eventName);
        public event OnCoreGameEvent CoreGameEvents;

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
    }
}
