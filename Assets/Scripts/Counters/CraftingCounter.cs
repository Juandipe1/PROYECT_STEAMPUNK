using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingCounter : BaseCounter
{
    [SerializeField] private CraftingRecipeSO[] craftingRecipeSOArray;

    public override void Interact(Player player)
    {
        if (!HasWorkshopObject())
        {
            // There is no WorkshopObject here
            if (player.HasWorkshopObject())
            {
                // Player is carring something
                if (HasRecipeWithInput(player.GetWorkshopObject().GetWorkshopObjectSO()))
                {
                    // Player carryng something that con be craft
                    player.GetWorkshopObject().SetWorkshopObjectParent(this);
                }
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
            }
            else
            {
                // Player is not carrying anything
                GetWorkshopObject().SetWorkshopObjectParent(player);
            }
        }
    }

    public override void InteractAternate(Player player)
    {
        if (HasWorkshopObject() && HasRecipeWithInput(GetWorkshopObject().GetWorkshopObjectSO()))
        {
            // There is a WorkshopObject here and it can be craft
            WorkshopObjectSO outputWorkshopObjectSO = GetOutputForInput(GetWorkshopObject().GetWorkshopObjectSO());

            GetWorkshopObject().DestroySelf();

            WorkshopObject.SpawnWorkshopObject(outputWorkshopObjectSO, this);
        }
    }

    private bool HasRecipeWithInput(WorkshopObjectSO inputWorkshopObjecSO)
    {
        foreach (CraftingRecipeSO craftingRecipeSO in craftingRecipeSOArray)
        {
            if (craftingRecipeSO.input == inputWorkshopObjecSO)
            {
                return true;
            }
        }
        return false;
    }

    private WorkshopObjectSO GetOutputForInput(WorkshopObjectSO inputWorkshopObjectSO)
    {
        foreach (CraftingRecipeSO craftingRecipeSO in craftingRecipeSOArray)
        {
            if (craftingRecipeSO.input == inputWorkshopObjectSO)
            {
                return craftingRecipeSO.output;
            }
        }

        return null;
    }
}
