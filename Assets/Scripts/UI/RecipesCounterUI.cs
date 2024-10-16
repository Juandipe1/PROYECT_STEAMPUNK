using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecipesCounterUI : MonoBehaviour
{
    private string recipeDeliveryText;
    private string minRecipesAmount;
    private int recipeAmount;
    private int minRecipeAmount;
    [SerializeField] private TextMeshProUGUI counterRecipesUI;

    void Update()
    {
        DeliveryCounterText();
    }

    private void DeliveryCounterText()
    {
        recipeDeliveryText = DeliveryManager.Instance.GetSuccesfulRecipesAmount().ToString();
        minRecipesAmount = GameOverUI.Instance.GetMinRecipesAmount().ToString();

        counterRecipesUI.text = recipeDeliveryText + "/" + minRecipesAmount;
        
        recipeAmount = DeliveryManager.Instance.GetSuccesfulRecipesAmount();
        minRecipeAmount = GameOverUI.Instance.GetMinRecipesAmount();

        if (recipeAmount >= minRecipeAmount)
        {
            counterRecipesUI.color = Color.green;
        }
    }
}
