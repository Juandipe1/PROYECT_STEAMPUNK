using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IWorkshopObjectParent
{
    public static Player Instance { get; private set; }

    public event EventHandler OnPickedSomething;
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private float moveSpeed = 7;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform WorkshopObjecHoldPoint;


    private bool isWalking;
    private Vector3 lastInteractDir;
    private BaseCounter selectedCounter;
    private WorkshopObject workshopObject;
    private CharacterController characterController;
    private float verticalSpeed = 2f;
    private float gravity = 9.81f;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is mero than one Player instance");
        }
        Instance = this;
    }

    void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
        characterController = GetComponent<CharacterController>();
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (!WorkshopGameManager.Instance.IsGamePlaying()) return;

        if (selectedCounter != null)
        {
            selectedCounter.InteractAternate(this);
        }
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (!WorkshopGameManager.Instance.IsGamePlaying()) return;

        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }
        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                // Has ClearCounter
                if (baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);

                    OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
                    {
                        selectedCounter = selectedCounter
                    });
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }
    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;

        if (characterController.isGrounded)
        {
            verticalSpeed = 0f; // El personaje está en el suelo, reinicia la velocidad vertical
        }
        else
        {
            // Aplicar gravedad
            verticalSpeed -= gravity * Time.deltaTime;
        }

        // Aplicar movimiento horizontal
        if (moveDir != Vector3.zero)
        {
            characterController.Move(moveDir * moveDistance);
            // Rotar hacia la dirección del movimiento
            float rotateSpeed = 10f;
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
        }

        // Aplicar movimiento vertical
        characterController.Move(new Vector3(0f, verticalSpeed * Time.deltaTime, 0f));

        isWalking = moveDir != Vector3.zero;
    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }

    public Transform GetWorkshopObjectFollowTransforms()
    {
        return WorkshopObjecHoldPoint;
    }

    public void SetWorkshopObject(WorkshopObject workshopObject)
    {
        this.workshopObject = workshopObject;

        if (workshopObject != null)
        {
            OnPickedSomething?.Invoke(this, EventArgs.Empty);
        }
    }

    public WorkshopObject GetWorkshopObject()
    {
        return workshopObject;
    }

    public void ClearWorkshopObject()
    {
        workshopObject = null;
    }

    public bool HasWorkshopObject()
    {
        return workshopObject != null;
    }
}
