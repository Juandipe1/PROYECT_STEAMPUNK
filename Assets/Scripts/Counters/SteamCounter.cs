using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamCounter : BaseCounter
{
    public static SteamCounter Instance { get; private set; }

    public event EventHandler OnPlayerGrabbedObject;

    [SerializeField] private WorkshopObjectSO workshopObjectSO;
    private float addValue = 10f;

    void Awake()
    {
        Instance = this;
    }

    public override void Interact(Player player)
    {
        if (player.HasWorkshopObject())
        {
                player.GetWorkshopObject().DestroySelf();

                WorkshopGameManager.Instance.AddPlayingTimer(addValue);
            
        }
    }
}
