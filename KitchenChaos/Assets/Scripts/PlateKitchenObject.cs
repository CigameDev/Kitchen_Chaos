using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// cai dia co co che dac biet,khong giong voi KitchenObject thong thuong
/// co the dat do an len cai dia
/// Nho vao plate thay doi lai cac thong so
/// </summary>
public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs :EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }

    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;
    /*
     * Nhung object ma Plate co the nhat duoc
     * bread,tomatoslices,cabbageslices,meatpattycooked,meatpattyburned
     */
    private List<KitchenObjectSO> kitchenObjectSOList;

    private void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)//Nguyen lieu
    {
        if(!validKitchenObjectSOList.Contains(kitchenObjectSO))//khong chua trong nhung thu co the dat dc len dia
            return false;
        if (kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            return false;
            //vi du Plate da co tomato roi thi khong nhat duoc them tomato nua
        }
        else
        {
            kitchenObjectSOList.Add(kitchenObjectSO);
            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
            {
                kitchenObjectSO = kitchenObjectSO
            }) ;
            return true;
        }
    }    
    public List<KitchenObjectSO>GetKitchenObjectSOList()
    {
        return kitchenObjectSOList;
    }    
}
