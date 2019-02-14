using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KitchenStatus
{
    READY,
    IN_PROGRESS,
    FINISHED
}

public class Kitchen : Interactables {

    public GameObject kitchenModel;

    private KitchenStatus currentStatus = KitchenStatus.READY;
    private FoodItem currentOrder = null;
    private float currentTimer = 0.0f;
    private Dictionary<KitchenStatus, Action<GameObject>> kitchenFunctions;

    // Use this for initialization
    void Start() {
        kitchenFunctions = new Dictionary<KitchenStatus, Action<GameObject>>();
        kitchenFunctions.Add(KitchenStatus.READY, ShowKitchenMenu);
        kitchenFunctions.Add(KitchenStatus.FINISHED, SurrenderOrder);
    }

    // Update is called once per frame
    void Update() {
        if (currentStatus == KitchenStatus.IN_PROGRESS)
        {
            currentTimer += Time.deltaTime;
            if (currentTimer >= currentOrder.TimeToMake)
            {
                FulfillOrder();
                currentTimer = 0;
            }
        }
    }

    private void FulfillOrder()
    {
        AudioSource audioSrc = GetComponent<AudioSource>();
        audioSrc.PlayOneShot(audioSrc.clip);
        currentStatus = KitchenStatus.FINISHED;
        kitchenModel.GetComponent<Renderer>().material.color = Color.green;
    }

    public void SurrenderOrder(GameObject player)
    {
        player.GetComponent<PlayerInventory>().SetInventory(currentOrder);
        currentOrder = null;
        kitchenModel.GetComponent<Renderer>().material.color = Color.white;
        currentStatus = KitchenStatus.READY;
    }

    public void ShowKitchenMenu(GameObject player)
    {
        GetComponent<KitchenMenu>().ShowMenu();
    }

    //OrderType?
    public void ReceiveOrder(FoodItem orderName)
    {
        currentOrder = orderName;
        kitchenModel.GetComponent<Renderer>().material.color = Color.red;
        currentStatus = KitchenStatus.IN_PROGRESS;

        GetComponent<KitchenMenu>().CloseMenu();
    }

    public override void TriggeredByPlayer(GameObject player)
    {
        if (kitchenFunctions.ContainsKey(currentStatus))
        {
            kitchenFunctions[currentStatus].Invoke(player);
        }
    }


}
