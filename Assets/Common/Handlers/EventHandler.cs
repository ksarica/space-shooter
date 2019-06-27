using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


namespace KS.Common.GameEvents
{
    public enum GameEventType
    {
        OnPlayerDeath, // Load scene to EndGameMenu
        OnPlayerHealthChanged, // Update the hitPoint
        OnPlayerCollidedWithAsteroid, // Update the hitPoint of the player
        OnScoreChanged, // Update the currentScore.Text value
        OnBestScoreChanged, // Update the bestscore.Text value
        OnBulletShotChanged, // Update the bulletShot value
        OnBulletCollidedWithAsteroid, // Update bulletSuccessful value
        OnAsteroidHit, // 
        OnAsteroidDestroyed, // Update currentScore value
        OnPowerUpCollected, // Give something to player
        OnPowerUpActive, 
        OnGoldCollected, // Update gold value of the player
        OnGoldScoreChanged 
    }

    public class EventHandler : MonoBehaviour
    {
        public static EventHandler instance = null;
        private Dictionary<GameEventType, GameEvent> gameEventCache;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject); 
                gameEventCache = new Dictionary<GameEventType, GameEvent>();
                GameEventType[] allEventTypes = (GameEventType[])Enum.GetValues(typeof(GameEventType)).Cast<GameEventType>();
                foreach (GameEventType currentEventType in allEventTypes)
                {
                    gameEventCache.Add(currentEventType, new GameEvent());
                }
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        public void Subscribe(GameEventType gameEventType, OnGameEventPublished callback)
        {
            gameEventCache[gameEventType] += callback;
        }

        public void Unsubscribe(GameEventType gameEventType, OnGameEventPublished callback)
        {
            gameEventCache[gameEventType] -= callback;
        }

        public void PublishGameEvent(GameEventType gameEventType, string[] eventData)
        {
            gameEventCache[gameEventType].Publish(eventData);
        }
    }

}

