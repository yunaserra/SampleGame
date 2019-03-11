using UnityEngine;

public class KitchenMenu : MonoBehaviour {
    public Transform ContentPanel;
    public FoodItem[] Items;

    public GameObject KitchenButtonPrefab;
    
    void Start()
    {
        AddButtons();
        ContentPanel.gameObject.SetActive(false);
    }

    public void ShowMenu()
    {
        ContentPanel.gameObject.SetActive(true);
    }

    public void CloseMenu()
    {
        ContentPanel.gameObject.SetActive(false);
    }

    private void AddButtons()
    {
        for (int i = 0; i < Items.Length; ++i)
        {
            GameObject newBtn = Instantiate(KitchenButtonPrefab, ContentPanel);
            newBtn.GetComponent<KitchenMenuButton>().SetupData(Items[i]);
        }
    }
}
