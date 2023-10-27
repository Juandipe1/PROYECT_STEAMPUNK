using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IWorkshopObjectParent
{
    [SerializeField] private Transform counterToPoint;


    private WorkshopObject workshopObject;

    public virtual void Interact(Player player)
    {
        Debug.LogError("BaseCounter.Interact();");
    }

    public virtual void InteractAternate(Player player)
    {
        Debug.LogError("BaseCounter.InteractAlternate();");
    }

     public Transform GetWorkshopObjectFollowTransforms()
    {
        return counterToPoint;
    }

    public void SetWorkshopObject(WorkshopObject workshopObject)
    {
        this.workshopObject = workshopObject;
    }

    public WorkshopObject GetWorkshopObject()
    {
        return workshopObject;
    }

    public void ClearWorkshopObject()
    {
        workshopObject = null;
    }

    public bool HasWorkshopObject()
    {
        return workshopObject != null;
    }
}
