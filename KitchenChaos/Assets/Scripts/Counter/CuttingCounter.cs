using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter,IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventAgrs> OnProgressChanged;//luu y doan nay do ke thua IHasProgress
    public event EventHandler OnCut;
    public class OnProgressChangedEventAgrs : EventArgs
    {
        public float progressNormalized;
    }


    private int cuttingProgress;

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    public override void Interact(Player player)
    {
        if (HasKitchenObject())
        //There is kitchenobject here
        {
            if (player.HasKitchenObject())
            //player is carrying something
            {
                if(player.GetKitchenObject().TryGetPlateKitchenObject(out PlateKitchenObject plateKitchenObject))
                {
                    if(plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }    
                }    
            }
            else//player is not carrying anything
            {
                this.GetKitchenObject().SetKitchenObjectParrent(player);
            }
        }
        else
        //KitchenObject is not here
        {
            if (player.HasKitchenObject())
            //player is carrying something
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                    //object ma player cam phai la 1 trong nhung object trong mang cutting (tuc la co the cat dc moi de dc vao cuttingCounter)
                {
                    player.GetKitchenObject().SetKitchenObjectParrent(this);
                    cuttingProgress = 0;

                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventAgrs
                    {
                        progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                    }) ;
                }
            }
            else//player is not carrying anything
            {

            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        //neu CuttingCounter co kitchenObject && co the cut duoc
        {
            cuttingProgress++;
            OnCut?.Invoke(this, EventArgs.Empty);
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventAgrs
            {
                progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
            });

            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
            { 
            KitchenObjectSO kitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
            GetKitchenObject().DestroySelf();//clear kitchenobject khoi cuttingcounter va xoa no di

            KitchenObject.SpawnKitchenObject(kitchenObjectSO, this);//static
            }
        }    
    }
    private bool HasRecipeWithInput(KitchenObjectSO kitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(kitchenObjectSO);
        return cuttingRecipeSO != null;
    }    
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO kitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(kitchenObjectSO);
        if (cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.output;
        }
        else return null;
    }    
    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach(CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
                return cuttingRecipeSO;
        }
        return null;
    }    
}
