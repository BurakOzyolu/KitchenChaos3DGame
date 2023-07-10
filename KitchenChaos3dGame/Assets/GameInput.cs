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
        //Event içerisne metod atanýr. Bu metod bu event'in artýk bir member.
        //Bu event PlayerInputActions ateþlendiði zaman içerisine üye olduðu zaman bütün üye olan metodlar çalýþýr.
        //EventHandler ise bu event'in yakalanmasýný saðlar.
        //OnInteractAction event'in ateþlenmesini saðlar.
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
