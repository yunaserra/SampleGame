using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenMenuButton : MonoBehaviour
{
    private FoodItem representedItem;
    private const string KITCHEN_TAG = "Kitchen";

    public GameObject kitchen;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetupData(FoodItem item)
    {
        representedItem = item;
    }

    public void SendOrderToKitchen()
    {
        GameObject kitchen = GameObject.FindGameObjectWithTag(KITCHEN_TAG);
        kitchen.GetComponent<Kitchen>().ReceiveOrder(representedItem);
    }
}
