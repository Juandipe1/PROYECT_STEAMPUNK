using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{
    [SerializeField] private CraftWorkshopObject craftWorkshopObject;
    [SerializeField] private Transform iconTemplate;

    void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    void Start()
    {
        craftWorkshopObject.OnIngredientAdded += CraftWorkshopObject_OnIngredientAdded;
    }

    private void CraftWorkshopObject_OnIngredientAdded(object sender, CraftWorkshopObject.OnIngradientAddedEventArgs e)
    {
        UpdateVisual();
    }

    void UpdateVisual()
    {
        foreach (Transform child in transform)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }


        foreach (WorkshopObjectSO workshopObjectSO in craftWorkshopObject.GetWorkshopObjectSOList())
        {
            Transform iconTransform = Instantiate(iconTemplate, transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<PlateIconSingleUI>().SetWorkshopObjectSO(workshopObjectSO);
        }
    }
}
