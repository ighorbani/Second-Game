using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public Wave[] waves;
    public Transform[] spawnPoints;
    public int timeBetweenWaves;
    // public GameObject boss;
    public Transform bossSpawnPoint;

    public GameObject healthBar;

    private Wave currentWave;
    private int currentWaveIndex;
    private bool finishedSpawning;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(StartNextWave(currentWaveIndex));
    }

    private void Update()
    {
        if (finishedSpawning && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            finishedSpawning = false;
            if (currentWaveIndex + 1 < waves.Length)
            {
                currentWaveIndex++;
                StartCoroutine(StartNextWave(currentWaveIndex));
            }
            else
            {
                // Debug.Log("Game Finished!");
                // Instantiate(boss, bossSpawnPoint.position, bossSpawnPoint.rotation);
                // healthBar.SetActive(true);
            }
        }
    }

    private IEnumerator StartNextWave(int index)
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        StartCoroutine(SpawnWave(index));
    }

    private IEnumerator SpawnWave(int index)
    {
        currentWave = waves[index];

        for (var i = 0; i < currentWave.count; i++)
        {
            if (player == null) yield break;

            var randomEnemy = currentWave.enemies[Random.Range(0, currentWave.enemies.Length)];
            var randomSpot = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomSpot.position, randomSpot.rotation);

            if (i == currentWave.count - 1)
                finishedSpawning = true;
            else
                finishedSpawning = false;

            yield return new WaitForSeconds(currentWave.timeBetweenSpawns);
        }
    }

    [Serializable]
    public class Wave
    {
        public Enemy[] enemies;
        public int count;
        public float timeBetweenSpawns;
    }
}