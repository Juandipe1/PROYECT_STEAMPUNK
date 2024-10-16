using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlDetection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InputSystem.onDeviceChange += onDeviceChange;
    }

    private void onDeviceChange(InputDevice device, InputDeviceChange change)
    {
        if (change == InputDeviceChange.Added)
        {
            Debug.Log($"Se conectó: {device.name} ({device.displayName})");
        }
        else if (change == InputDeviceChange.Removed)
        {
            Debug.Log($"Se desconectó: {device.name} ({device.displayName})");
        }
    }

    
}
