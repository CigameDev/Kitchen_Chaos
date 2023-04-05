using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
/// <summary>
/// this script attaches to the kitchenobject prefab
/// ex:tomato,cheese
/// </summary>
public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    
    private IKitchenObjectParrent kitchenObjectParrent;//thay the cho ClearCounter truoc kia
    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }    
  
    public void SetKitchenObjectParrent(IKitchenObjectParrent kitchenObjectParrent)
    {
        if (this.kitchenObjectParrent !=null)
        {
            this.kitchenObjectParrent.ClearKitchenObject();
        }

        this.kitchenObjectParrent = kitchenObjectParrent;
        if(kitchenObjectParrent.HasKitchenObject())
        {
            Debug.LogError("KitchenObjectParrent already has a kitchen");
        }    
        kitchenObjectParrent.SetKitchenObject(this);
        transform.parent = kitchenObjectParrent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }   
    public IKitchenObjectParrent GetKitchenObjectParrent()
    {
        return this.kitchenObjectParrent;
    }    
    public void DestroySelf()
    {
        kitchenObjectParrent.ClearKitchenObject();
        Destroy(gameObject);
    }    
    public bool TryGetPlateKitchenObject( out PlateKitchenObject plateKitchenObject)
    {
        //funtion nay kiem tra xem KitchenObject co phai la PlateKitchenObject k,neu co thi lay no ra
        if(this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }    
        else
        {
            plateKitchenObject = null;
            return false;
        }    
    }    
    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO,IKitchenObjectParrent kitchenObjectParrent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParrent(kitchenObjectParrent);

        return kitchenObjectTransform.GetComponent<KitchenObject>();
    }    
}
