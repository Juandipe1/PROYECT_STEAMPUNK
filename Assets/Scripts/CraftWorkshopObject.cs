using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftWorkshopObject : WorkshopObject
{
    [SerializeField] private List<WorkshopObjectSO> validWorkshopSOList;

    private List<WorkshopObjectSO> workshopObjectSOList;

    void Awake()
    {
        workshopObjectSOList = new List<WorkshopObjectSO>();
    }

    public bool TryAddIngredient(WorkshopObjectSO workshopObjectSO)
    {
        if(!validWorkshopSOList.Contains(workshopObjectSO))
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
            return true;
        }
    }
}
