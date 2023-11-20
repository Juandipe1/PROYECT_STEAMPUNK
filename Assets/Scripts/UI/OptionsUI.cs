using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Button soundEffectsButtom;
    [SerializeField] private Button musicsButtom;
    [SerializeField] private Button closeButtom;
    [SerializeField] private Button moveUpButtom;
    [SerializeField] private Button moveDownButtom;
    [SerializeField] private Button moveLeftButtom;
    [SerializeField] private Button moveRightButtom;
    [SerializeField] private Button interactButtom;
    [SerializeField] private Button pauseButtom;
    [SerializeField] private Button gamepadInteractButtom;
    [SerializeField] private Button gamepadPauseButtom;
    [SerializeField] private TextMeshProUGUI soundEffectText;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private TextMeshProUGUI gamepadInteractText;
    [SerializeField] private TextMeshProUGUI gamepadPauseText;
    [SerializeField] private Transform pressToRebindKeyTransform;

    private Action onCloseButtomAction;

    void Awake()
    {
        Instance = this;

        soundEffectsButtom.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        musicsButtom.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        closeButtom.onClick.AddListener(() =>
        {
            Hide();
            onCloseButtomAction();
        });

        moveUpButtom.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Up); });
        moveDownButtom.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Down); });
        moveLeftButtom.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Left); });
        moveRightButtom.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Right); });
        interactButtom.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Interact); });
        pauseButtom.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Pause); });
        gamepadInteractButtom.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Interact); });
        gamepadInteractButtom.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Pause); });
    }

    void Start()
    {
        WorkshopGameManager.Instance.OnGameUnpaused += WorkshopGameManager_OnGameUnpaused;
        UpdateVisual();

        HidePressToRebindKey();
        Hide();
    }

    private void WorkshopGameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        soundEffectText.text = "Efectos de Sonido: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicText.text = "Música: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);

        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        gamepadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact);
        gamepadPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Pause);
    }

    public void Show(Action onCloseButtomAction)
    {
        this.onCloseButtomAction = onCloseButtomAction;
        gameObject.SetActive(true);

        soundEffectsButtom.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ShowPressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(true);
    }
    private void HidePressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(false);
    }

    private void RebindBinding(GameInput.Binding binding)
    {
        ShowPressToRebindKey();
        GameInput.Instance.RebindBinding(binding, () =>
        {
            HidePressToRebindKey();
            UpdateVisual();
        });
    }
}
