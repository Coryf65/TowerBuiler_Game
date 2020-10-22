using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameSceneManager
{

    // List of all of our Scenes
    public enum Scene
    {
        GameScene,
        MainMenuScene,
    }

    public static void Load(Scene scene)
    {
        // Reset the time
        Time.timeScale = 1f;
        // load a scene
        SceneManager.LoadScene(scene.ToString());
    }
}
