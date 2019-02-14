using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour {
    private FoodItem inventoryContent;
    private float currentMoney;

    public GameObject trayUI;
    public GameObject moneyUI;

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
        trayUI.GetComponent<Image>().sprite = null;
        trayUI.GetComponent<Image>().color = Color.clear;
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
        trayUI.GetComponent<Image>().sprite = inventoryContent.Icon;
        trayUI.GetComponent<Image>().color = Color.white;
    }

    public void AddMoney(float amount)
    {
        currentMoney += amount;
        moneyUI.GetComponent<Text>().text = currentMoney.ToString();
    }
}
