using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;

    void Start()
    {
        WorkshopGameManager.Instance.OnStateChanged += WorkshopGameManager_OnStateChanged;
        
        Hide();
    }

    private void WorkshopGameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (WorkshopGameManager.Instance.IsCountdownToStartActive())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    void Update()
    {
        countdownText.text = Mathf.Ceil(WorkshopGameManager.Instance.GetCountdownToStartTimer()).ToString();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
