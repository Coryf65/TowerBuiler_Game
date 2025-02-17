﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsUI : MonoBehaviour
{
    // getting the sound and Music manager to interact with
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private MusicManager musicManager;

    private TextMeshProUGUI soundVolumeText;
    private TextMeshProUGUI musicVolumeText;

    public static OptionsUI Instance { get; private set; }

    public void Awake()
    {
        Instance = this;

        soundVolumeText = transform.Find("soundText").Find("currentVolumeText").GetComponent<TextMeshProUGUI>();
        musicVolumeText = transform.Find("musicText").Find("currentMusic").GetComponent<TextMeshProUGUI>();

        // Sound | Volume Click Events
        transform.Find("soundText").Find("soundPlusBtn").GetComponent<Button>().onClick.AddListener(() => {
            soundManager.IncreaseVolume();
            UpdateText();
        });
        transform.Find("soundText").Find("soundMinusBtn").GetComponent<Button>().onClick.AddListener(() => {
            soundManager.DecreaseVolume();
            UpdateText();
        });

        // Music Click Events
        transform.Find("musicText").Find("musicPlusBtn").GetComponent<Button>().onClick.AddListener(() => {
            musicManager.IncreaseVolume();
            UpdateText();
        });
        transform.Find("musicText").Find("musicMinusBtn").GetComponent<Button>().onClick.AddListener(() => {
            musicManager.DecreaseVolume();
            UpdateText();
        });

        // Edge Scroll setting
        transform.Find("edgeScrollToggle").GetComponent<Toggle>().onValueChanged.AddListener((bool set) => {
            CameraHandler.Instance.SetEdgeScrolling(set);
        });       

        // Main Menu Button
        transform.Find("mainMenuBtn").GetComponent<Button>().onClick.AddListener(() => {
            GameSceneManager.Load(GameSceneManager.Scene.MainMenuScene);
        });
    }

    private void Start()
    {
        UpdateText();
        gameObject.SetActive(false);

        transform.Find("edgeScrollToggle").GetComponent<Toggle>().SetIsOnWithoutNotify(CameraHandler.Instance.GetEdgeScrolling());
    }

    // updating the text for volumes
    private void UpdateText()
    {
        soundVolumeText.SetText(Mathf.RoundToInt(soundManager.GetCurrentVolume() * 100).ToString()); // * 10 to make it easier to read
        musicVolumeText.SetText(Mathf.RoundToInt(musicManager.GetCurrentVolume() * 100).ToString()); // * 10 to make it easier to read
    }

    public void ToggleVisible()
    {
        gameObject.SetActive(!gameObject.activeSelf);

        // Pausing the game when open
        if (gameObject.activeSelf)
        {
            Time.timeScale = 0f;
        } else
        {
            // and unpause
            Time.timeScale = 1f;
        }
    }
}
