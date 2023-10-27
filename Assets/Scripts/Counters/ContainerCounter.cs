using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter, IWorkshopObjectParent
{

    public event EventHandler OnPlayerGrabbedObject;

    [SerializeField] private WorkshopObjectSO workshopObjectSO;


    public override void Interact(Player player)
    {
        if (!player.HasWorkshopObject())
        {
            // Player is not carrying anything
            WorkshopObject.SpawnWorkshopObject(workshopObjectSO, player);

            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }

    }

}
