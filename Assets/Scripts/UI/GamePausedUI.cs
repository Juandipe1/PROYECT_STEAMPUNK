using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePausedUI : MonoBehaviour
{

    [SerializeField] private Button resumeButtom;
    [SerializeField] private Button mainMenuButtom;
    [SerializeField] private Button optionsButtom;

    void Awake()
    {
        resumeButtom.onClick.AddListener(() =>
        {
            WorkshopGameManager.Instance.TogglePauseGame();
        });
        mainMenuButtom.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        optionsButtom.onClick.AddListener(() =>
        {
            Hide();
            OptionsUI.Instance.Show(Show);
        });
    }

    void Start()
    {
        WorkshopGameManager.Instance.OnGamePaused += WorkshopGameManager_OnGamePasued;
        WorkshopGameManager.Instance.OnGameUnpaused += WorkshopGameManager_OnGameUnpasued;

        Hide();
    }

    private void WorkshopGameManager_OnGameUnpasued(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void WorkshopGameManager_OnGamePasued(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);


        resumeButtom.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
