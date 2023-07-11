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
        //1- Kullanýcý "E" tuþuna basdýðý zaman burada Interact_Performed() metodu tetiklenecek.
        //2- Bu metod OnInteractAction?.Invoke(this, EventArgs.Empty) ile ilgili eventi tetikler.
        //3- OnInteractAction referansý kullananan bütün metodlar bu event tetiklendiðinde çalýþacaktýr.
        //4- gameInput.OnInteractAction += GameInputOnInteraction;  ile Deneme metodu bu event'e dahil olur.
        //5- GameInputOnInteraction() metodu içeriisinde yazan kodlar bu event tetiklediðinde artýk çalýþacaktýr.

        //Event içerisne metod atanýr. Bu metod bu event'in artýk bir member.
        //Bu event PlayerInputActions ateþlendiði zaman içerisine üye olduðu zaman bütün üye olan metodlar çalýþýr.
        //EventHandler ise Event'i referans eden deðer.
        //OnInteractAction event'in ateþlenmesini saðlar.
        //Action: Herhangi bir deðer dönmeyen void.

    }
    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    void Start()
    {
        
    }

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
