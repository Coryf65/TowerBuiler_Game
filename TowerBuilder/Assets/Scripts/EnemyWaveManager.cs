using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{

    public static EnemyWaveManager Instance { get; private set; }

    // creating an event to handler the wave changes and update the ui
    public event EventHandler OnWaveNumberChanged;

    // creating a State like design
    private enum State
    {
        WaitingToSpawnNextWave,
        SpawningWave,
    }

    [Header("Spawn Points")]
    [Tooltip("Place a EnemySpawnPosition GameObject on map for more spawn points")]
    [SerializeField] private List<Transform> spawnPositionTransforms;
    [Tooltip("Connected to the nextWaveSpawnPoint GameObject, displays next spawn area")]
    [SerializeField] private Transform nextSpawnPositionTransform;
    [Header("Spawner Settings")][Space]    
    [SerializeField] private State currentState;
    [SerializeField] private int waveNumber;

    private float nextWaveSpawnTimer;
    private float enemySpawnTimer;
    [SerializeField] private int enemiesToSpawnCount;
    Vector3 spawnPosition;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // setting State
        currentState = State.WaitingToSpawnNextWave;
        spawnPosition = spawnPositionTransforms[UnityEngine.Random.Range(0, spawnPositionTransforms.Count)].position; // randomy choose a spawn point from our list
        nextSpawnPositionTransform.position = spawnPosition;
        nextWaveSpawnTimer = 3f;
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.WaitingToSpawnNextWave:
                nextWaveSpawnTimer -= Time.deltaTime;
                if (nextWaveSpawnTimer < 0f)
                {
                    SpawnWave();
                }
                break;
            case State.SpawningWave:
                // check how many enemies we have left to spawn
                if (enemiesToSpawnCount > 0)
                {
                    enemySpawnTimer -= Time.deltaTime;
                    if (enemySpawnTimer < 0f)
                    {
                        // Reset the timer
                        enemySpawnTimer = UnityEngine.Random.Range(0f, .2f);
                        // Spawn an enemy
                        Enemy.Create(spawnPosition + UtilsClass.GetRandomDirection() * UnityEngine.Random.Range(0f, 10f));
                    }
                    // we spawned one change counter
                    enemiesToSpawnCount--;

                    if (enemiesToSpawnCount <= 0)
                    {
                        currentState = State.WaitingToSpawnNextWave;
                        spawnPosition = spawnPositionTransforms[UnityEngine.Random.Range(0, spawnPositionTransforms.Count)].position; // randomy choose a spawn point from our list
                        nextSpawnPositionTransform.position = spawnPosition;
                        nextWaveSpawnTimer = 10f; // 10 seconds
                    }
                }
                break;
        }               
    }

    private void SpawnWave()
    {                
        enemiesToSpawnCount = (5 + 3 * waveNumber); // building a dynamic spawn counter
        currentState = State.SpawningWave;
        waveNumber++;
        OnWaveNumberChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    ///  Gets the Wave number to use in our UI
    /// </summary>
    /// <returns>The Current Wave Number</returns>
    public int GetWaveNumber()
    {
        return waveNumber;
    }

    /// <summary>
    ///  Gets the Current Spawner Timer
    /// </summary>
    /// <returns>The next Wave Spawn Timer</returns>
    public float GetWaveSpawnTimer()
    {
        return nextWaveSpawnTimer;
    }

    public Vector3 GetSpawnPosition()
    {
        return spawnPosition;
    }

}
