using UnityEngine;
using UnityEngine.UI;

public class KitchenMenuButton : MonoBehaviour
{
    public GameObject IconImg;
    private FoodItem representedItem;
    private const string KITCHEN_TAG = "Kitchen";

    public void SetupData(FoodItem item)
    {
        representedItem = item;
        IconImg.GetComponent<Image>().sprite = item.Icon;
    }

    public void SendOrderToKitchen()
    {
        GameObject kitchen = GameObject.FindGameObjectWithTag(KITCHEN_TAG);
        kitchen.GetComponent<Kitchen>().ReceiveOrder(representedItem);
    }
}
