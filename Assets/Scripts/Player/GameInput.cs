using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private const string PLAYER_PREFS_BINDINGS = "InputBindigs";

    public static GameInput Instance { get; private set; }

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;

    public enum Binding
    {
        Move_Up,
        Move_Down,
        Move_Left,
        Move_Right,
        Interact,
        Pause,
        Gamepad_Interact,
        Gamepad_Pause
    }

    private PlayerController playerController;

    void Awake()
    {
        Instance = this;

        playerController = new PlayerController();
        playerController.PlayerControls.Enable();

        playerController.PlayerControls.Interact.performed += Interact_performed;
        playerController.PlayerControls.InteractAlternative.performed += InteractAlternate_performed;
        playerController.PlayerControls.Pause.performed += Pause_Performed;

        if(PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
        {
            playerController.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
        }
    }

    void OnDestroy()
    {
        playerController.PlayerControls.Interact.performed -= Interact_performed;
        playerController.PlayerControls.InteractAlternative.performed -= InteractAlternate_performed;
        playerController.PlayerControls.Pause.performed -= Pause_Performed;

        playerController.Dispose();
    }

    private void Pause_Performed(InputAction.CallbackContext context)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(InputAction.CallbackContext context)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(InputAction.CallbackContext context)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerController.PlayerControls.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }

    public string GetBindingText(Binding binding)
    {
        switch (binding)
        {
            default:
            case Binding.Move_Up:
                return playerController.PlayerControls.Move.bindings[1].ToDisplayString();
            case Binding.Move_Down:
                return playerController.PlayerControls.Move.bindings[2].ToDisplayString();
            case Binding.Move_Left:
                return playerController.PlayerControls.Move.bindings[3].ToDisplayString();
            case Binding.Move_Right:
                return playerController.PlayerControls.Move.bindings[4].ToDisplayString();
            case Binding.Interact:
                return playerController.PlayerControls.Interact.bindings[0].ToDisplayString();
            case Binding.Pause:
                return playerController.PlayerControls.Pause.bindings[0].ToDisplayString();
            case Binding.Gamepad_Interact:
                return playerController.PlayerControls.Interact.bindings[1].ToDisplayString();
            case Binding.Gamepad_Pause:
                return playerController.PlayerControls.Pause.bindings[1].ToDisplayString();
        }
    }

    public void RebindBinding(Binding binding, Action onActionRebound)
    {
        playerController.PlayerControls.Disable();

        InputAction inputAction;
        int bindingIndex;

        switch (binding)
        {
            default:
            case Binding.Move_Up:
                inputAction = playerController.PlayerControls.Move;
                bindingIndex = 1;
                break;
            case Binding.Move_Down:
                inputAction = playerController.PlayerControls.Move;
                bindingIndex = 2;
                break;
            case Binding.Move_Left:
                inputAction = playerController.PlayerControls.Move;
                bindingIndex = 3;
                break;
            case Binding.Move_Right:
                inputAction = playerController.PlayerControls.Move;
                bindingIndex = 4;
                break;
            case Binding.Interact:
                inputAction = playerController.PlayerControls.Interact;
                bindingIndex = 0;
                break;
            case Binding.Pause:
                inputAction = playerController.PlayerControls.Pause;
                bindingIndex = 0;
                break;
            case Binding.Gamepad_Interact:
                inputAction = playerController.PlayerControls.Interact;
                bindingIndex = 1;
                break;
            case Binding.Gamepad_Pause:
                inputAction = playerController.PlayerControls.Pause;
                bindingIndex = 1;
                break;
        }

        inputAction.PerformInteractiveRebinding(bindingIndex)
            .OnComplete((callback) =>
            {
                callback.Dispose();
                playerController.PlayerControls.Enable();
                onActionRebound();

                Debug.Log(playerController.SaveBindingOverridesAsJson());
                PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, playerController.SaveBindingOverridesAsJson());
                PlayerPrefs.Save();
            })
            .Start();
    }
}
