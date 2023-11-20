using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    void Start()
    {
        WorkshopGameManager.Instance.OnStateChanged += WorkshopGameManager_OnStateChanged;

        Show();
    }

    private void WorkshopGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (WorkshopGameManager.Instance.IsCountdownToStartActive())
        {
            Hide();
        }
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
