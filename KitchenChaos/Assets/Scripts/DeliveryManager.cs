using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler RecipeSpawned;
    public event EventHandler RecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;
    public static DeliveryManager Instance { get; private set; }

    /// <summary>
    /// tai sao can tao scriptableobject RecipeSOList de chua danh sach cac mon an
    /// Ma khong tao list nhu binh thuong
    /// Vi ta co the de dang them hoac xoa mon an ,khong lam anh huong toi code
    /// </summary>
    [SerializeField] private RecipeSOList recipeListSO;

    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer = 4f;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipeMax = 4;
    private int successfullRecipesAmount;

    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;
            if (KitchenGameManager.Instance.IsGamePlaying() && waitingRecipeSOList.Count < waitingRecipeMax)
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];

                waitingRecipeSOList.Add(waitingRecipeSO);

                RecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)//giao thuc an 
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++)//duyet list mon an sinh ra ngau nhien
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                //so luong kitchenObject o mon an thu i khop voi so luong kitchenobject o cai dia
                bool plateContentsMatchesRecipe = true;//check xem mon an o dia co dung la 1 mon an trong list ko
                foreach(KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    bool ingredientFound = false;//check kitchenobject co nam trong dia khong
                    foreach(KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        if(plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            ingredientFound = true;
                            break;
                        }    
                    }    
                    if(!ingredientFound)
                    {
                        plateContentsMatchesRecipe = false;
                    }    
                }    
                if(plateContentsMatchesRecipe)//thuc an trong dia khop voi 1 mon trong list
                {
                    Debug.Log("giao hang dung mon an "+waitingRecipeSO.name);

                    waitingRecipeSOList.RemoveAt(i);
                    successfullRecipesAmount++;

                    RecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }    
            }
        }
        //khong chinh xac
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetRecipeSOList()
    {
        return waitingRecipeSOList;
    }    
    public int GetSuccessfullRecipesAmount()
    {
        return successfullRecipesAmount;
    }    
}
