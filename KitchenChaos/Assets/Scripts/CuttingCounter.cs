using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    public event EventHandler<OnProgressChangedEventAgrs> OnProgressChanged;
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
                    cuttingProgress = 0;

                    CuttingRecipeSO cuttingRecipeSO = CuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventAgrs
                    {
                        progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                    }) ;
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
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        //neu CuttingCounter co kitchenObject && co the cut duoc
        {
            cuttingProgress++;
            OnCut?.Invoke(this, EventArgs.Empty);
            CuttingRecipeSO cuttingRecipeSO = CuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

            OnProgressChanged?.Invoke(this, new OnProgressChangedEventAgrs
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
        CuttingRecipeSO cuttingRecipeSO = CuttingRecipeSOWithInput(kitchenObjectSO);
        return cuttingRecipeSO != null;
    }    
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO kitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = CuttingRecipeSOWithInput(kitchenObjectSO);
        if (cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.output;
        }
        else return null;
    }    
    private CuttingRecipeSO CuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach(CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
                return cuttingRecipeSO;
        }
        return null;
    }    
}
