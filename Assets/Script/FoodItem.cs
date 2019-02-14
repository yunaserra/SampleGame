using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New FoodItem", menuName = "Food Item")]
public class FoodItem : ScriptableObject {
    [SerializeField]
    private string itemName;
    [SerializeField]
    private int itemPrice;
    [SerializeField]
    private Sprite icon;
    [SerializeField]
    private float timeToMake = 2.0f;

    public string ItemName
    {
        get { return itemName; }
    }

    public int ItemPrice
    {
        get { return itemPrice; }
    }

    public Sprite Icon
    {
        get { return icon; }
    }

    public float TimeToMake
    {
        get { return timeToMake; }
    }
}
