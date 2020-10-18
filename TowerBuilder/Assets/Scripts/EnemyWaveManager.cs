using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    private float nextWaveSpawnTimer;
    private float enemySpawnTimer;
    private int enemiesToSpawnCount;
    Vector3 spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        nextWaveSpawnTimer = 3f;
    }

    private void Update()
    {
        nextWaveSpawnTimer -= Time.deltaTime;
        if (nextWaveSpawnTimer < 0)
        {
            SpawnWave();
        }

        // check how many enemies we have left to spawn
        if (enemiesToSpawnCount > 0)
        {
            enemySpawnTimer -= Time.deltaTime;
            if (enemySpawnTimer < 0)
            {
                // Reset the timer
                enemySpawnTimer = Random.Range(0, .2f);
                // Spawn an enemy
                Enemy.Create(spawnPosition + UtilsClass.GetRandomDirection() * Random.Range(0f, 10f));
            }
            // we spawned one change counter
            enemiesToSpawnCount--;
        }
    }

    private void SpawnWave()
    {
        spawnPosition = new Vector3(40, 0);

        for (int count = 0; count < 10; count++)
        {
            nextWaveSpawnTimer = 10f; // 10 seconds
            enemiesToSpawnCount = 10; // 10 enemies total
        }
    }

}
