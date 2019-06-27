using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Common.ObjectPooling;

public class LevelController : MonoBehaviour
{
    [SerializeField] private GameObject asteroidPrefab;
    [SerializeField] private GameObject goldPrefab;
    [SerializeField] private GameObject powerUpPrefab1;
    [SerializeField] private GameObject powerUpPrefab2;
    [SerializeField] private float delayInSeconds;
    [SerializeField] private float spawnIntervalInSeconds;
    [SerializeField] private SpawnRange spawnRange;
    private ObjectPool asteroidPool;
    private ObjectPool powerUpPool1;
    private ObjectPool powerUpPool2;
    private ObjectPool goldPool;


    [Serializable]
    public struct SpawnRange
    {
        public float min;
        public float max;
    }

    // Start is called before the first frame update
    void Start()
    {
        asteroidPool = new ObjectPool(asteroidPrefab);
        powerUpPool1 = new ObjectPool(powerUpPrefab1);
        powerUpPool2 = new ObjectPool(powerUpPrefab2);
        goldPool = new ObjectPool(goldPrefab);
        StartCoroutine(Delay(delayInSeconds));
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
            if (innerDice < 50)
            {
                powerUpPool1.CreateAtPosition(new Vector3(UnityEngine.Random.Range(spawnRange.min, spawnRange.max), 20, 0), Quaternion.identity); // you have to add UnityEngine because there is two random functions first one is Unity Random the other is C# random itself
            }
            else
            {
                powerUpPool2.CreateAtPosition(new Vector3(UnityEngine.Random.Range(spawnRange.min, spawnRange.max), 20, 0), Quaternion.identity); // you have to add UnityEngine because there is two random functions first one is Unity Random the other is C# random itself
            }
        }
        else if(dice >= 5 && dice < 9)
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
