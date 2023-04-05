using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

  
    public override void Interact(Player player)
    {
        if(HasKitchenObject())
            //There is kitchenobject here
        {
            if(player.HasKitchenObject())
                //player is carrying something
            {
                if(player.GetKitchenObject().TryGetPlateKitchenObject(out PlateKitchenObject plateKitchenObject) )//neu player dang cam 1 cai dia
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))//them vao list thuc an
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }    
                else
                //player is not carrying PlateKitchenObject but something else
                {
                    if(GetKitchenObject().TryGetPlateKitchenObject(out plateKitchenObject))
                    {
                        //counter is holding plate
                        //trong vong if khong khai bao them PlateKitchenObject ma su dung luon,vi van nam trong vong if truoc
                        if(plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }    
                    }    
                }    
            }    
            else
            //player is not carrying anything
            {
                this.GetKitchenObject().SetKitchenObjectParrent(player);
            }
        }   
        else
        //There is not kitchenobject here
        {
            if(player.HasKitchenObject())
                //player is carrying something
            {
                player.GetKitchenObject().SetKitchenObjectParrent(this);
            }   
            else
            //Player is not carrying anything
            {

            }    
        }    
    }
   
}
