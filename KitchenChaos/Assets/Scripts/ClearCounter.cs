using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour,IKitchenObjectParrent
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject;//dung cach nay de chi moi clearcounter chi sinh ra 1 object duy nhat(tomato)

    
    /*
     * public void Interact
     * kich ban 
     * nhan E de Interact
     * neu chua co kitchenObject nao thi sinh ra 1 cai tren cai ban,setkitchenobject (this) la cai ban
     * neu tiep tuc nhan E thi Interct thi SetKitchenObjectParrent cho player
     */
    public void Interact(Player player)
    {
        Debug.Log("Interact");
        if (kitchenObject == null)//neu o A chua co qua ca chua nao thi sinh ra qua ca chua 
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParrent(this);
        }else
        {
            //give the object to the player
            kitchenObject.SetKitchenObjectParrent(player);//player ke thua IKitchenObjectParrent nen no cung la KitchenObjectParrent
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
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
