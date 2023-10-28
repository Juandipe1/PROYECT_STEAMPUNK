using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public static DeliveryCounter Instance { get; private set; }

    public event EventHandler OnPlayerGrabbedObject;

    [SerializeField] private WorkshopObjectSO workshopObjectSO;

    void Awake()
    {
        Instance = this;
    }

    public override void Interact(Player player)
    {
        if (player.HasWorkshopObject())
        {
            if (player.GetWorkshopObject().TryGetCraft(out CraftWorkshopObject craftWorkshopObject))
            {
                // Only acepts plates/crafts
                DeliveryManager.Instance.DeliverRecipe(craftWorkshopObject);

                player.GetWorkshopObject().DestroySelf();
                // Player is not carrying anything
                WorkshopObject.SpawnWorkshopObject(workshopObjectSO, player);

                OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
