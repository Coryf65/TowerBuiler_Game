using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SpawnWave();
    }

    private void SpawnWave()
    {
        Vector3 spawnPosition = new Vector3(20, 0);

        for (int count = 0; count < 10; count++)
        {
            // adding some randomness to the generator
            Enemy.Create(spawnPosition + UtilsClass.GetRandomDirection() * Random.Range(0f, 10f));
        }
    }

}
