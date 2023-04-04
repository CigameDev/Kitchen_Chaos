using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter
{
    private enum State
    {
        Idle,
        Frying,
        Fried,
        Burned
    }    
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

    private State state;
    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;
    private float fryingTimer;
    private float burningTimer;

    private void Start()
    {
        state = State.Idle;
    }
    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTimer += Time.deltaTime;
                    if (fryingTimer >= fryingRecipeSO.fryingTimerMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
                        //destroy di mieng thit song di,sinh ra mieng thit chin
                        state = State.Fried;//chien,neu sau qua trinh nay se bi chay(burned)
                        burningTimer = 0f;
                        burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    }
                    break;
                case State.Fried:
                    burningTimer += Time.deltaTime;
                    if (burningTimer >= burningRecipeSO.burningTimerMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);
                        //destroy di mieng thit chin di,sinh ra mieng thit chay
                        state = State.Burned;
                    }
                    break;
                case State.Burned:
                    break;

            }
        }
       
    }
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
            //player khong co kitchenObject,lay kitchenobject do ra
            {
                this.GetKitchenObject().SetKitchenObjectParrent(player);
                state = State.Idle;
            }
        }
        else
        //quay khong co kitchenObject
        {
            if (player.HasKitchenObject())
            //player co kitchenObject
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                //object ma player cam phai la 1 trong nhung object trong mang frying (tuc la co the cat dc moi de dc vao cuttingCounter)
                {
                    player.GetKitchenObject().SetKitchenObjectParrent(this);
                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    state = State.Frying;
                    fryingTimer = 0f;
                }
            }
            else
            //player khong co kitchenObject
            {

            }
        }
    }
    
    private bool HasRecipeWithInput(KitchenObjectSO kitchenObjectSO)
    {
        //kiem tra xem co dat duoc vao cai lo(Stove) khong
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(kitchenObjectSO);
        return fryingRecipeSO != null;
    }
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO kitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(kitchenObjectSO);
        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.output;
        }
        else return null;
    }
    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            if (fryingRecipeSO.input == inputKitchenObjectSO)
                return fryingRecipeSO;
        }
        return null;
    }
    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach(BurningRecipeSO burningRecipeSO in burningRecipeSOArray)
        {
            if(burningRecipeSO.input == inputKitchenObjectSO)
                return burningRecipeSO;
        }
        return null;
    }    
}
