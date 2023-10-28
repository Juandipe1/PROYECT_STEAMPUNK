using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;


    void Awake()
    {
        playButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.Tutorial);
        });
        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }

    public void PlayClick()
    {
        Loader.Load(Loader.Scene.Tutorial);
    }

    public void QuitClick()
    {
        Application.Quit();
    }
}
