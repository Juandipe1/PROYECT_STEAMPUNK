using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private Player player;
    private float footseopTimer;
    private float footseopTimerMax = .25f;

    void Awake()
    {
        player = GetComponent<Player>();
    }

    void Update()
    {
        footseopTimer -= Time.deltaTime;
        if (footseopTimer < 0f)
        {
            footseopTimer = footseopTimerMax;

            if (player.IsWalking())
            {
                float volume = 6f;
                SoundManager.Instance.PlayFootsepsSound(player.transform.position, volume);
            }
        }
    }
}
