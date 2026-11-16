using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() => {
            KitchenGameManager.Instance.TogglePauseGame();
        });

        mainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
    }

    private void Start()
    {
        KitchenGameManager.Instance.OnGamePaused += GameInput_GamePaused;
        KitchenGameManager.Instance.OnGameUnpaused += GameInput_GameUnpaused;

        hide();
    }

    private void GameInput_GameUnpaused(object sender, EventArgs e)
    {
        hide();
    }

    private void GameInput_GamePaused(object sender, EventArgs e)
    {
        show();
    }

    private void show()
    {
        gameObject.SetActive(true);
    }

    private void hide()
    {
        gameObject.SetActive(false);
    }
}
