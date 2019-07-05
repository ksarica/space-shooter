using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Assets.Common.ObjectPooling;
using KS.Common.GameEvents;
using EventHandler = KS.Common.GameEvents.EventHandler;
using KS.Actor.Controllers;
using KS.Actor.Movement;
using KS.Actor.Health;

public class LevelController : MonoBehaviour
{
    // This component is responsible for all spawns

    [SerializeField] private GameObject goldPrefab;
    [SerializeField] private GameObject asteroidPrefab;
    [SerializeField] private GameObject powerUpRateSpeed;
    [SerializeField] private GameObject powerUpDoubleGun;
    [SerializeField] private GameObject powerUpRepair;
    [SerializeField] private float difficultyRatio;
    [SerializeField] private float delayInSeconds;
    [SerializeField] private float spawnIntervalInSeconds;
    [SerializeField] private SpawnRange spawnRange;
    private ObjectPool asteroidPool;
    private ObjectPool powerUpRateSpeedPool;
    private ObjectPool powerUpDoubleGunPool;
    private ObjectPool powerUpRepairPool;
    private ObjectPool goldPool;
    private int scoreCounter = 0;
    private int functionCallCounter = 0;

    [Serializable]
    public struct SpawnRange
    {
        public float min;
        public float max;
    }

    // Start is called before the first frame update
    void Start()
    {
        EventHandler.instance.Subscribe(GameEventType.OnScoreChanged, ChangeDifficulty);
        asteroidPool = new ObjectPool(asteroidPrefab);
        powerUpRateSpeedPool = new ObjectPool(powerUpRateSpeed);
        powerUpDoubleGunPool = new ObjectPool(powerUpDoubleGun);
        powerUpRepairPool = new ObjectPool(powerUpRepair);
        goldPool = new ObjectPool(goldPrefab);
        StartCoroutine(Delay(delayInSeconds));
    }

    private void OnDestroy()
    {
        EventHandler.instance.Unsubscribe(GameEventType.OnScoreChanged, ChangeDifficulty);
    }

    private void ChangeDifficulty(string[] values)
    {
        MovementSystem movement = asteroidPrefab.GetComponent<MovementSystem>();
        int score = int.Parse(values[0]);
        Debug.Log("score: " + score + " scoreCounter: " + scoreCounter + " functionCallCounter: " + functionCallCounter);
        Debug.Log("minSpeed: " + movement.GetMinSpeed() + " maxSpeed: " + movement.GetMaxSpeed());
        if ((score - scoreCounter) > 40)
        {
            functionCallCounter++;
            scoreCounter += 40;
            if (functionCallCounter % 2 == 0)
            {
                Debug.Log("Asteroid yönü değişecek: " + asteroidPrefab.GetComponent<AIController>().changeDirection);
                asteroidPrefab.GetComponent<AIController>().changeDirection = true;
                Debug.Log("Asteroid yönü değişti: " + asteroidPrefab.GetComponent<AIController>().changeDirection);
            }
            else
            {
                asteroidPrefab.GetComponent<AIController>().changeDirection = false;
            }
            movement.SetMinSpeed(movement.GetMinSpeed() * difficultyRatio);
            movement.SetMaxSpeed(movement.GetMaxSpeed() * difficultyRatio);
        }

    }

    public IEnumerator Delay(float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        yield return StartCoroutine(WaitAndSpawn(spawnIntervalInSeconds));
    }

    public IEnumerator WaitAndSpawn(float waitBeforeSpawnInSeconds)
    {
        yield return new WaitForSeconds(waitBeforeSpawnInSeconds);
        // spawn edebiliriz..
        float dice = UnityEngine.Random.Range(0f, 100f);
        if (dice < 5)
        {
            float innerDice = UnityEngine.Random.Range(0f, 100f);
            if (innerDice < 33)
            {
                powerUpRateSpeedPool.CreateAtPosition(new Vector3(UnityEngine.Random.Range(spawnRange.min, spawnRange.max), 20, 0), Quaternion.identity); // you have to add UnityEngine because there is two random functions first one is Unity Random the other is C# random itself
            }
            else if (innerDice >= 33 && innerDice < 66)
            {
                powerUpDoubleGunPool.CreateAtPosition(new Vector3(UnityEngine.Random.Range(spawnRange.min, spawnRange.max), 20, 0), Quaternion.identity); // you have to add UnityEngine because there is two random functions first one is Unity Random the other is C# random itself
            }
            else
            {
                powerUpRepairPool.CreateAtPosition(new Vector3(UnityEngine.Random.Range(spawnRange.min, spawnRange.max), 20, 0), Quaternion.identity); // you have to add UnityEngine because there is two random functions first one is Unity Random the other is C# random itself
            }
        }
        else if (dice >= 5 && dice < 9)
        {
            goldPool.CreateAtPosition(new Vector3(UnityEngine.Random.Range(spawnRange.min, spawnRange.max), 20, 0), Quaternion.identity);
        }
        else
        {
            asteroidPool.CreateAtPosition(new Vector3(UnityEngine.Random.Range(spawnRange.min, spawnRange.max), 20, 0), Quaternion.identity); // you have to add UnityEngine because there is two random functions first one is Unity Random the other is C# random itself
        }

        spawnIntervalInSeconds = spawnIntervalInSeconds * 0.95f;
        spawnIntervalInSeconds = Mathf.Max(spawnIntervalInSeconds, 0.3f); // returns largest of two values

        yield return StartCoroutine(WaitAndSpawn(spawnIntervalInSeconds));
    }
}
