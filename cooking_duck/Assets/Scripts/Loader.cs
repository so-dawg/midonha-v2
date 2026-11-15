using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene { MainMenuScene, GameScene, LoadingScene}
    private static Scene target;

    public static void Load(Scene scene)
    {
        target = scene;
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    // Wait until the loading screen is loaded then load the game scene
    public static void LoadCallback()
    {
        SceneManager.LoadScene(target.ToString());
    }
}
