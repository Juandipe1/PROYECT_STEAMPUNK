using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CraftWorkshopObject : WorkshopObject
{
    public static event EventHandler OnAnyCraft;

    public static void ResetStaticData()
    {
        OnAnyCraft = null;
    }

    public event EventHandler<OnIngradientAddedEventArgs> OnIngredientAdded;
    public class OnIngradientAddedEventArgs : EventArgs
    {
        public WorkshopObjectSO workshopObjectSO;
    }

    [SerializeField] private List<WorkshopObjectSO> validWorkshopSOList;
    [SerializeField] private GameObject objectToDeactivate;
    [SerializeField] private GameObject objectToActivate;

    private List<WorkshopObjectSO> workshopObjectSOList;

    void Awake()
    {
        workshopObjectSOList = new List<WorkshopObjectSO>();
        objectToActivate.SetActive(false);
    }

    public bool TryAddIngredient(WorkshopObjectSO workshopObjectSO)
    {
        if (!validWorkshopSOList.Contains(workshopObjectSO))
        {
            // Not a valid Ingredient
            return false;
        }
        if (workshopObjectSOList.Contains(workshopObjectSO))
        {
            // Already has this type
            return false;
        }
        else
        {
            workshopObjectSOList.Add(workshopObjectSO);

            OnIngredientAdded?.Invoke(this, new OnIngradientAddedEventArgs
            {
                workshopObjectSO = workshopObjectSO
            });

            if (workshopObjectSOList.Count == validWorkshopSOList.Count)
            {
                if (objectToDeactivate != null)
                {
                    objectToDeactivate.SetActive(false);
                }

                if (objectToActivate != null)
                {
                    objectToActivate.SetActive(true);
                    OnAnyCraft?.Invoke(this, EventArgs.Empty);
                }
            }

            return true;
        }
    }

    public List<WorkshopObjectSO> GetWorkshopObjectSOList()
    {
        return workshopObjectSOList;
    }
}
