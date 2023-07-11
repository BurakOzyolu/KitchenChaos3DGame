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
        //1- Kullan�c� "E" tu�una basd��� zaman burada Interact_Performed() metodu tetiklenecek.
        //2- Bu metod OnInteractAction?.Invoke(this, EventArgs.Empty) ile ilgili eventi tetikler.
        //3- OnInteractAction referans� kullananan b�t�n metodlar bu event tetiklendi�inde �al��acakt�r.
        //4- gameInput.OnInteractAction += GameInputOnInteraction;  ile Deneme metodu bu event'e dahil olur.
        //5- GameInputOnInteraction() metodu i�eriisinde yazan kodlar bu event tetikledi�inde art�k �al��acakt�r.

        //Event i�erisne metod atan�r. Bu metod bu event'in art�k bir member.
        //Bu event PlayerInputActions ate�lendi�i zaman i�erisine �ye oldu�u zaman b�t�n �ye olan metodlar �al���r.
        //EventHandler ise Event'i referans eden de�er.
        //OnInteractAction event'in ate�lenmesini sa�lar.
        //Action: Herhangi bir de�er d�nmeyen void.

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
