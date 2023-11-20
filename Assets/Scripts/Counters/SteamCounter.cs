using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamCounter : BaseCounter
{
    public static event EventHandler OnSteamCharge;

    new public static void ResetStaticData()
    {
        OnSteamCharge = null;
    }

    [SerializeField] private WorkshopObjectSO specificWorkshopObjectSO;

    private float addTime = 8f;

    public override void Interact(Player player)
    {
        if (player.HasWorkshopObject())
        {
            if (player.GetWorkshopObject().GetWorkshopObjectSO() == specificWorkshopObjectSO)
            {
                player.GetWorkshopObject().DestroySelf();

                WorkshopGameManager.Instance.AddPlayingTimer(addTime);

                OnSteamCharge?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
