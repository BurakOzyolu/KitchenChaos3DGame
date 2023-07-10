using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;
    [SerializeField] GameInput gameInput;
    bool isWalking;
    private Vector3 lastInteraction;
    [SerializeField] LayerMask countersLayermask;


    void Start()
    {
        
    }

    void Update()
    {
        HandleMovement();
        HandleInteractions();
    }
    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRaduis = 0.7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRaduis, moveDir, moveDistance);

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRaduis, moveDirX, moveDistance);

            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRaduis, moveDirZ, moveDistance);
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
            if (rayCastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                Debug.Log("Interact");
            }
        }
        else
        {
            Debug.Log("-");
        }
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}

