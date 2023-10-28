using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePausedUI : MonoBehaviour
{

    void Start()
    {
        WorkshopGameManager.Instance.OnGamePaused += WorkshopGameManager_OnGamePasued;
        WorkshopGameManager.Instance.OnGameUnpaused += WorkshopGameManager_OnGameUnpasued;

        Hide();
    }

    private void WorkshopGameManager_OnGameUnpasued(object sender, EventArgs e)
    {
        Hide();
    }

    private void WorkshopGameManager_OnGamePasued(object sender, EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
