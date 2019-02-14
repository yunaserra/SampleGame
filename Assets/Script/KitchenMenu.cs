using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KitchenMenu : MonoBehaviour {
    public Transform contentPanel;
    public FoodItem[] items;

    public GameObject kitchenButtonPrefab;

    // Use this for initialization
    void Start() {
        AddButtons();
        contentPanel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update() {

    }

    public void ShowMenu()
    {
        Debug.Log("Show menu!");
        contentPanel.gameObject.SetActive(true);
    }

    public void CloseMenu()
    {
        contentPanel.gameObject.SetActive(false);
    }

    private void AddButtons()
    {
        for (int i = 0; i < items.Length; ++i)
        {
            GameObject newBtn = Instantiate(kitchenButtonPrefab, contentPanel);
            // Yuna TODO: make these buttons flow in a "grid layout"
            newBtn.GetComponent<KitchenMenuButton>().SetupData(items[i]);
            newBtn.GetComponentInChildren<Text>().text = items[i].ItemName;
            //newBtn.transform.SetParent(contentPanel);
        }
    }
}
