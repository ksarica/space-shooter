using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Common.ObjectPooling;

public class LevelController : MonoBehaviour
{
    [SerializeField] private GameObject asteroidPrefab;
    [SerializeField] private float delayInSeconds;
    [SerializeField] private float spawnIntervalInSeconds;
    [SerializeField] private SpawnRange spawnRange;
    private ObjectPool asteroidPool;
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
        asteroidPool.CreateAtPosition(new Vector3(UnityEngine.Random.Range(spawnRange.min, spawnRange.max), 20, 0));
        spawnIntervalInSeconds = spawnIntervalInSeconds * 0.95f;
        spawnIntervalInSeconds = Mathf.Max(spawnIntervalInSeconds, 0.3f);

        yield return StartCoroutine(WaitAndSpawn(spawnIntervalInSeconds));
    }
}
