using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    public override void Interact(Player player)
    {
        if (HasKitchenObject())
        //quay co kitchenObject
        {
            if (player.HasKitchenObject())
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
            if (player.HasKitchenObject())
            //player co kitchenObject
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                    //object ma player cam phai la 1 trong nhung object trong mang cutting (tuc la co the cat dc moi de dc vao cuttingCounter)
                {
                    player.GetKitchenObject().SetKitchenObjectParrent(this);
                }
            }
            else
            //player khong co kitchenObject
            {

            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if(HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
            //neu CuttingCounter co kitchenObject && co the cut duoc
        {
            KitchenObjectSO kitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
            GetKitchenObject().DestroySelf();//clear kitchenobject khoi cuttingcounter va xoa no di

            KitchenObject.SpawnKitchenObject(kitchenObjectSO, this);//static
        }    
    }
    private bool HasRecipeWithInput(KitchenObjectSO kitchenObjectSO)
    {
        foreach(CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (kitchenObjectSO == cuttingRecipeSO.input)
                return true;
        }    
        return false;
    }    
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO kitchenObjectSO)
    {
        foreach(CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (kitchenObjectSO == cuttingRecipeSO.input)
                return cuttingRecipeSO.output;
        }
        return null;
    }    
}
