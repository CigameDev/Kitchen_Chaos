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
        /*
        * 2 clear counter A va B
        * sinh ra 1 kitchenobject tai A(tien cham vao A va nhan E)
        * nhan phim T(de chuyen kitchenobject tu A sang B)
        * khi chuyen can set kitchenobject tai A bang null
        * neu tai B da co 1 kitchenobject roi thi khong chuyen dc sang (error)
        * dat vi tri cua kitchenobject vao transform dat san
        * set localposition ve zero
        */
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
}
