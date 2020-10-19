using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyWaveUI : MonoBehaviour
{
    [SerializeField] private EnemyWaveManager enemyWaveManager;
    private TextMeshProUGUI waveNumberText;
    private TextMeshProUGUI waveMaessageText;
    private RectTransform enemyWaveSpawnerIndicator;
    private Camera mainCamera;

    private void Awake()
    {
        waveNumberText = transform.Find("waveNumberText").GetComponent<TextMeshProUGUI>();
        waveMaessageText = transform.Find("waveMessageText").GetComponent<TextMeshProUGUI>();
        enemyWaveSpawnerIndicator = transform.Find("enemyWaveSpawnerIndicator").GetComponent<RectTransform>();
    }

    private void Start()
    {
        mainCamera = Camera.main;
        enemyWaveManager.OnWaveNumberChanged += EnemyWaveManager_OnWaveNumberChanged;
        SetWaveNumberText("Wave " + enemyWaveManager.GetWaveNumber());
    }

    private void EnemyWaveManager_OnWaveNumberChanged(object sender, System.EventArgs e)
    {
        SetWaveNumberText("Wave " + enemyWaveManager.GetWaveNumber());
    }

    private void Update()
    {
        HandleNextWaveMessage();

        HandleEnemyIndicator();
    }

    private void HandleNextWaveMessage()
    {
        float nextWaveSpawnTimer = enemyWaveManager.GetWaveSpawnTimer();
        if (nextWaveSpawnTimer <= 0f)
        {
            SetMessageText("");
        }
        else
        {
            SetMessageText("Next Wave in " + nextWaveSpawnTimer.ToString("F1") + "s");
        }
    }

    private void HandleEnemyIndicator()
    {
        Vector3 directionToNextEnemySpawn = (enemyWaveManager.GetSpawnPosition() - mainCamera.transform.position).normalized;
        // offsetting the image
        enemyWaveSpawnerIndicator.anchoredPosition = directionToNextEnemySpawn * 400f;
        // rotate the image on the Z axis to spin the indicator
        enemyWaveSpawnerIndicator.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(directionToNextEnemySpawn));

        float distanceToSpawnPosition = Vector3.Distance(enemyWaveManager.GetSpawnPosition(), mainCamera.transform.position);
        // Hiding the indicator if we are close
        enemyWaveSpawnerIndicator.gameObject.SetActive(distanceToSpawnPosition > mainCamera.orthographicSize * 1.5f);
    }

    private void SetMessageText(string message)
    {
        waveMaessageText.SetText(message);
    }

    private void SetWaveNumberText(string text)
    {
        waveNumberText.SetText(text);
    }
}
