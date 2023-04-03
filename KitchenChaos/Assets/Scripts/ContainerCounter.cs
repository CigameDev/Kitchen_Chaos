using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ContainerCounter : BaseCounter,IKitchenObjectParrent
{
    public event EventHandler OnPlayerGrabbedKitchen;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
            //player dang khong cam kitchenObject nao thi moi duoc lay
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParrent(player);
            OnPlayerGrabbedKitchen?.Invoke(this, EventArgs.Empty);
        }
    }

}
