using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter,IKitchenObjectParrent
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    
    /*
     * public void Interact
     * kich ban 
     * nhan E de Interact
     * neu chua co kitchenObject nao thi sinh ra 1 cai tren cai ban,setkitchenobject (this) la cai ban
     * neu tiep tuc nhan E thi Interct thi SetKitchenObjectParrent cho player
     */
    public override void Interact(Player player)
    {
        if(HasKitchenObject())
            //quay co kitchenObject
        {
            if(!player.HasKitchenObject())
                //player khong co kitchenObject
            {
                this.GetKitchenObject().SetKitchenObjectParrent(player);
            }    
        }   
        else
        //quay khong co kitchenObject
        {
            if(player.HasKitchenObject())
                //player co kitchenObject
            {
                player.GetKitchenObject().SetKitchenObjectParrent(this);
            }    
        }    
    }
   
}
