using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour {
    public GameObject TrayUI;
    public GameObject MoneyUI;

    private FoodItem inventoryContent;
    private float currentMoney;

    public FoodItem TakeCurrentInventory()
    {
        FoodItem temp = inventoryContent;
        inventoryContent = null;

        return inventoryContent;
    }

    public bool DoesPlayerHave(FoodItem item)
    {
        return inventoryContent.ItemName == item.ItemName;
    }

    public void EmptyInventory()
    {
        inventoryContent = null;
        TrayUI.GetComponent<Image>().sprite = null;
        TrayUI.GetComponent<Image>().color = Color.clear;
    }

    public void SetInventory(FoodItem newItem)
    {
        Assert.IsFalse(inventoryContent, "Inventory content is set");
        if (!inventoryContent)
        {
            inventoryContent = newItem;
            // Update image
            UpdateTrayUI();
        }
    }

    private void UpdateTrayUI()
    {
        TrayUI.GetComponent<Image>().sprite = inventoryContent.Icon;
        TrayUI.GetComponent<Image>().color = Color.white;
    }

    public void AddMoney(float amount)
    {
        currentMoney += amount;
        MoneyUI.GetComponent<Text>().text = currentMoney.ToString();
    }
}
