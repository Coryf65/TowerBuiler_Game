using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{

    public static GameOverUI Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        // creating a click event
        transform.Find("retryButton").GetComponent<Button>().onClick.AddListener(() => {
            GameSceneManager.Load(GameSceneManager.Scene.GameScene);
        });



        transform.Find("mainMenuButton").GetComponent<Button>().onClick.AddListener(() => {
            GameSceneManager.Load(GameSceneManager.Scene.MainMenuScene);
        });

        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);

        transform.Find("waveText").GetComponent<TextMeshProUGUI>().SetText($"You Survived {EnemyWaveManager.Instance.GetWaveNumber()} Waves!");
        Time.timeScale = .1f;
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
