using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct WorkshopObjectSO_GameObject
    {
        public WorkshopObjectSO workshopObjectSO;
        public GameObject gameObject;
    }

    [SerializeField] private CraftWorkshopObject craftWorkshopObject;
    [SerializeField] private List<WorkshopObjectSO_GameObject> workshopObjectSOGameObjectList;

    void Start()
    {
        craftWorkshopObject.OnIngredientAdded += craftWorkshopObject_OnIngredientAdded;

        foreach (WorkshopObjectSO_GameObject workshopObjectGameObject in workshopObjectSOGameObjectList)
        {
                workshopObjectGameObject.gameObject.SetActive(false);
        }
    }

    private void craftWorkshopObject_OnIngredientAdded(object sender, CraftWorkshopObject.OnIngradientAddedEventArgs e)
    {
        foreach (WorkshopObjectSO_GameObject workshopObjectGameObject in workshopObjectSOGameObjectList)
        {
            if (workshopObjectGameObject.workshopObjectSO == e.workshopObjectSO)
            {
                workshopObjectGameObject.gameObject.SetActive(true);
            }
        }
    }
}
