using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{

    public override void Interact(Player player)
    {
        if (!HasWorkshopObject())
        {
            // There is no WorkshopObject here
            if (player.HasWorkshopObject())
            {
                // Player is carring something
                player.GetWorkshopObject().SetWorkshopObjectParent(this);
            }
            else
            {
                // Player not carrying anything
            }
        }
        else
        {
            // There is a WorshopObject here
            if (player.HasWorkshopObject())
            {
                // Player is carrying something
                if (player.GetWorkshopObject().TryGetCraft(out CraftWorkshopObject craftWorkshopObject))
                {
                    // Player is holdind a craft
                    if (craftWorkshopObject.TryAddIngredient(GetWorkshopObject().GetWorkshopObjectSO()))
                    {
                        GetWorkshopObject().DestroySelf();
                    }
                }
                else
                {
                    // Player is not carrying a craft but something else
                    if (GetWorkshopObject().TryGetCraft(out craftWorkshopObject))
                    {
                        // Counter is holding a craft
                        if (craftWorkshopObject.TryAddIngredient(player.GetWorkshopObject().GetWorkshopObjectSO()))
                        {
                            player.GetWorkshopObject().DestroySelf();
                        }
                    }
                }
            }
            else
            {
                // Player is not carrying anything
                GetWorkshopObject().SetWorkshopObjectParent(player);
            }
        }
    }


}
