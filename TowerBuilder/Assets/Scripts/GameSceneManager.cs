using System.Collections;
using System.Collections.Generic;
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
        // load a scene
        SceneManager.LoadScene(scene.ToString());
    }
}
