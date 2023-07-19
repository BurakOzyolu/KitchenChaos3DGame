using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangeEventArg> OnSelectedCounterChangeEvent;

    public class OnSelectedCounterChangeEventArg : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;
    [SerializeField] GameInput gameInput;
    bool isWalking;
    private Vector3 lastInteraction;
    [SerializeField] LayerMask countersLayermask;
    private BaseCounter selectedCounter;
    //(Ctrl + R + R) bir deðiþkeni bu class içerisinde deðiþtirilmesini saðlar.

    [SerializeField] Transform kitchenObjectHoldPoint;

    [SerializeField] KitchenObject kitchenObject;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("There is more than one Player instance");
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
        gameInput.OnInteractAction += GameInputOnInteraction;
        gameInput.OnInteractAlternateAction += GameInputOnInteractAlternatieAction;
    }

    private void GameInputOnInteractAlternatieAction(object sender, EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
            transform.LookAt(selectedCounter.transform);
        }
    }



    void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    private void GameInputOnInteraction(object sender, EventArgs e)
    {
          if (selectedCounter !=null)
          {
              selectedCounter.Interact(this);
              transform.LookAt(selectedCounter.transform);
        }
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRaduis = 0.7f;
        float playerHeight = 2f;
        bool canMove =  !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRaduis, moveDir, moveDistance);

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
            canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRaduis, moveDirX, moveDistance);

            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
                canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRaduis, moveDirZ, moveDistance);
                if (canMove)
                {
                    moveDir = moveDirZ;
                }
                else
                {
                    //
                }

            }
        }
        if (canMove)
        {
            transform.position += moveDir * moveDistance;

            isWalking = moveDir != Vector3.zero;

            transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);
        }
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInteraction = moveDir;
        }

        float interactDistance = 2f;
        //RaycastHit : Raycast'e çarpan objeyi temsil eder
        //interactDistance : Raycast' etkileþim için gereken uzunluk
        //lastInteraction : Objenin hangi yönde olduðunu belirtir.
        //countersLayermask : Etkileþime geçen objeinin layerini temsil eder.


        if (Physics.Raycast(transform.position, lastInteraction, out RaycastHit rayCastHit, interactDistance, countersLayermask))
        {
            if (rayCastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
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

    public bool IsWalking()
    {
        return isWalking;
    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChangeEvent?.Invoke(this, new OnSelectedCounterChangeEventArg
        {
            selectedCounter = selectedCounter
        });
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}

