using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallback : MonoBehaviour
{
    private bool isFirstsUpdate = true;

    void Update()
    {
        if(isFirstsUpdate)
        {
            isFirstsUpdate = false;

            Loader.LoaderCallback();
        }
    }
}

