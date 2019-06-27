using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Common.GameEvents
{
    public delegate void OnGameEventPublished(string[] eventData);
    public class GameEvent
    {
        protected event OnGameEventPublished registeredFunctions;

        public void Publish(string[] eventData)
        {
            if (registeredFunctions != null && registeredFunctions.GetInvocationList().Length > 0)
            {
                registeredFunctions(eventData);
            }
        }

        public static GameEvent operator +(GameEvent targetEvent , OnGameEventPublished targetDelegate)
        {
            targetEvent.registeredFunctions += targetDelegate;
            return targetEvent;
        }

        public static GameEvent operator -(GameEvent targetEvent, OnGameEventPublished targetDelegate)
        {
            targetEvent.registeredFunctions -= targetDelegate;
            return targetEvent;
        }
    }
}
