using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    // creating a State like design
    private enum State
    {
        WaitingToSpawnNextWave,
        SpawningWave,
    }
    [SerializeField] private Transform spawnPositionTransform;
    [SerializeField] private State currentState;
    private float nextWaveSpawnTimer;
    private float enemySpawnTimer;
    private int enemiesToSpawnCount;
    Vector3 spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        // setting State
        currentState = State.WaitingToSpawnNextWave;

        nextWaveSpawnTimer = 3f;
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.WaitingToSpawnNextWave:
                nextWaveSpawnTimer -= Time.deltaTime;
                if (nextWaveSpawnTimer < 0)
                {
                    SpawnWave();
                }
                break;
            case State.SpawningWave:
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

                    if (enemiesToSpawnCount <= 0)
                    {
                        currentState = State.WaitingToSpawnNextWave;
                    }
                }
                break;
        }               
    }

    private void SpawnWave()
    {
        spawnPosition = spawnPositionTransform.position;
        nextWaveSpawnTimer = 10f; // 10 seconds
        enemiesToSpawnCount = 10; // 10 enemies total
        currentState = State.SpawningWave;
    }

}
