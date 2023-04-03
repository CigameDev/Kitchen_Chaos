using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

  
    public override void Interact(Player player)
    {
        if(HasKitchenObject())
            //quay co kitchenObject
        {
            if(player.HasKitchenObject())
                //player co kitchenObject
            {

            }    
            else
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
            else
            //player khong co kitchenObject
            {

            }    
        }    
    }
   
}
