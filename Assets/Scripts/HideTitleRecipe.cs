using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class HideTitleRecipe : MonoBehaviour
{
    [SerializeField] private float timer;
    [SerializeField] private GameObject titulo;
    // Start is called before the first frame update
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            titulo.gameObject.SetActive(false);
            timer = 0f;
        }
    }

}
