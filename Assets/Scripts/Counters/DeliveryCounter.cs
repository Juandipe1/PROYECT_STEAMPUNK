using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (player.HasWorkshopObject())
        {
            if (player.GetWorkshopObject().TryGetCraft(out CraftWorkshopObject craftWorkshopObject))
            {
                // Only acepts plates/crafts
                DeliveryManager.Instance.DeliverRecipe(craftWorkshopObject);
                
                player.GetWorkshopObject().DestroySelf();
            }
        }
    }
}
