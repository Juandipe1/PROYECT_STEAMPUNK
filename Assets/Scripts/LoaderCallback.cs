using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Asegúrate de importar el namespace necesario

public class LoaderCallback : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LoadSceneAfterDelay(20f)); // Espera 2 segundos antes de cargar la escena
    }

    IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Loader.LoaderCallback();
    }
}

