using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkshopObject : MonoBehaviour
{
    [SerializeField] private WorkshopObjectSO workshopObjectSO;

    private IWorkshopObjectParent workshopObjectParent;

    public WorkshopObjectSO GetWorkshopObjectSO()
    {
        return workshopObjectSO;
    }

    public void SetWorkshopObjectParent(IWorkshopObjectParent workshopObjectParent)
    {
        if (this.workshopObjectParent != null)
        {
            this.workshopObjectParent.ClearWorkshopObject();
        }

        this.workshopObjectParent = workshopObjectParent;

        if (workshopObjectParent.HasWorkshopObject())
        {
            Debug.LogError("IWorkshopObjectParent already has a WorkshopObject!");
        }
        workshopObjectParent.SetWorkshopObject(this);

        transform.parent = workshopObjectParent.GetWorkshopObjectFollowTransforms();
        transform.localPosition = Vector3.zero;
    }

    public IWorkshopObjectParent GetWorkshopObjectParent()
    {
        return workshopObjectParent;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public bool TryGetCraft(out CraftWorkshopObject craftWorkshopObject)
    {
        if (this is CraftWorkshopObject)
        {
            craftWorkshopObject = this as CraftWorkshopObject;
            return true;
        }
        else
        {
            craftWorkshopObject = null;
            return false;
        }
    }

    public static WorkshopObject SpawnWorkshopObject(WorkshopObjectSO workshopObjectSO, IWorkshopObjectParent workshopObjectParent)
    {
        Transform objectTransform = Instantiate(workshopObjectSO.prefab);
        WorkshopObject workshopObject  = objectTransform.GetComponent<WorkshopObject>();
        
        workshopObject.SetWorkshopObjectParent(workshopObjectParent);

        return workshopObject;
    }
}
