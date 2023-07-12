using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] KitchenObjectSO KitchenObjectSO;
    [SerializeField] Transform counterTopPoint;

    [SerializeField] KitchenObject kitchenObject;
    [SerializeField] ClearCounter secondClearCounter;
    [SerializeField] bool testing;
    void Start()
    {
        
    }

    void Update()
    {
        if (testing && Input.GetKeyDown(KeyCode.T))
        {
            if (kitchenObject != null)
            {
                kitchenObject.SetClearCounter(secondClearCounter);
            }
        }
    }

    public void Interact ()
    {
        if (kitchenObject == null)
        {
            Transform kitchenObject = Instantiate(KitchenObjectSO.prefab, counterTopPoint);
            kitchenObject.GetComponent<KitchenObject>().SetClearCounter(this);
        }
        else
        {
            Debug.Log(kitchenObject.GetClearCounter());
        }

    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }
    public KitchenObject GetKitchenObject() 
    {
        return this.kitchenObject;
    }
    public void ClearKitchenObject()
    {
        this.kitchenObject = null;
    }
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
