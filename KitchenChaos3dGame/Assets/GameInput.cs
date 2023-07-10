using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Intract.performed += Interact_performed;
        //Event i�erisne metod atan�r. Bu metod bu event'in art�k bir member.
        //Bu event PlayerInputActions ate�lendi�i zaman i�erisine �ye oldu�u zaman b�t�n �ye olan metodlar �al���r.
        //EventHandler ise bu event'in yakalanmas�n� sa�lar.
        //OnInteractAction event'in ate�lenmesini sa�lar.
    }
    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;
        
        return inputVector;
    }
}
