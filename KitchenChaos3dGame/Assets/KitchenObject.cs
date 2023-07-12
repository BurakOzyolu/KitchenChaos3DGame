using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    [SerializeField] private ClearCounter clearCounter;

    public KitchenObjectSO GetKitchenObjectSO() { return kitchenObjectSO; }

    public void SetClearCounter(ClearCounter clearCounter)
    { 
        if (this.clearCounter != null)
        {
            clearCounter.ClearKitchenObject();
        }
        this.clearCounter = clearCounter;

        if (this.clearCounter.HasKitchenObject())
        {
            Debug.Log("Clear counter alredy has a kitchen Object!");
        }
        clearCounter.SetKitchenObject(this);

        transform.parent = clearCounter.GetKitchenObjectFollowTransform();

        transform.localPosition = Vector3.zero;
    }

    public ClearCounter GetClearCounter() { return clearCounter; }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
