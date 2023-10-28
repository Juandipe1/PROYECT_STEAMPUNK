using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI1 : MonoBehaviour
{
    [SerializeField] private Button playButton;


    void Awake()
    {
        playButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(1);
        });
    }

    public void PlayClick()
    {
        SceneManager.LoadScene(1);
    }
}
