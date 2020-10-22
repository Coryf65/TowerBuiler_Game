using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuUI : MonoBehaviour
{

    public void Awake()
    {
        transform.Find("MainMenuUI").Find("PlayBtn").GetComponent<Button>().onClick.AddListener(() => {
            GameSceneManager.Load(GameSceneManager.Scene.GameScene);
        });

        transform.Find("MainMenuUI").Find("QuitBtn").GetComponent<Button>().onClick.AddListener(() => {
            Application.Quit(); // works as an Exe
        });
    }

    
}
