using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipesDeliveredText;
    [SerializeField] private int minRecipesAmountGame;
    [SerializeField] private Button buttonSuccessGame;
    [SerializeField] private Button buttonFailed;

    [SerializeField] private string nextScene;

    void Awake()
    {
        buttonSuccessGame.onClick.AddListener(() =>
        {
            Loader.Load((Loader.Scene)Enum.Parse(typeof(Loader.Scene), nextScene));
        });
        buttonFailed.onClick.AddListener(() =>
        {
            ResetScene();
        });
    }

    void Start()
    {
        WorkshopGameManager.Instance.OnStateChanged += WorkshopGameManager_OnStateChanged;

        buttonSuccessGame.gameObject.SetActive(false);
        buttonFailed.gameObject.SetActive(false);

        Hide();
    }

    private void WorkshopGameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (WorkshopGameManager.Instance.IsGameOver())
        {
            Show();

            recipesDeliveredText.text = DeliveryManager.Instance.GetSuccesfulRecipesAmount().ToString();
            if (DeliveryManager.Instance.GetSuccesfulRecipesAmount() >= minRecipesAmountGame)
            {
                buttonSuccessGame.gameObject.SetActive(true);
                buttonSuccessGame.Select();
            }
            else
            {
                buttonFailed.gameObject.SetActive(true);
                buttonFailed.Select();
            }
        }
        else
        {
            Hide();
        }
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
