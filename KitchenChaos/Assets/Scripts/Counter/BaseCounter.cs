using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour,IKitchenObjectParrent
{
    public static event EventHandler OnAnyObjectPlaceHere;

    public static void ResetStaticData()
    {
        OnAnyObjectPlaceHere = null;
    }

    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject;
    public virtual void Interact(Player player)
    {
        Debug.LogError("BaseCounter.LogError-Interact");
    }

    public virtual void InteractAlternate(Player player)
    {
        //Debug.LogError("BaseCounter.LogError-InteractAlternate");
    }
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if(kitchenObject != null)
        {
            OnAnyObjectPlaceHere?.Invoke(this, EventArgs.Empty);
        }    
    }
    public KitchenObject GetKitchenObject()
    {
        return this.kitchenObject;
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
