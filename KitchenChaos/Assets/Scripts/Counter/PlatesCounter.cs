using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlatesSpawned;
    public event EventHandler OnPlatesRemoved;
    [SerializeField] private KitchenObjectSO platesKitchenObjectSO;

    private float spawnPlatesTimer;
    private float spawnPlatesTimerMax = 4f;
    private int platesSpawnAmount;
    private int platesSpawnAmountMax = 4;

    private void Update()
    {
        spawnPlatesTimer += Time.deltaTime;
        if(spawnPlatesTimer >= spawnPlatesTimerMax)
        {
            spawnPlatesTimer = 0f;
            if (platesSpawnAmount < platesSpawnAmountMax)
            {
                platesSpawnAmount++;
                OnPlatesSpawned?.Invoke(this, EventArgs.Empty);
            }
        }    
    }
    public override void Interact(Player player)
    {
        if(!player.HasKitchenObject())
        {
            if(platesSpawnAmount >0)
            {
                platesSpawnAmount--;
                KitchenObject.SpawnKitchenObject(platesKitchenObjectSO,player);
                OnPlatesRemoved?.Invoke(this, EventArgs.Empty);
            }    
        }    
    }
}
