using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public enum CustomerState
{
    WALKING,
    WAITING_TO_ORDER,
    WAITING_FOR_ORDER,
    EATING,
    WAITING_TO_PAY,
    LEAVING,
}

public class Customer : Interactables
{
    public GameObject orderMarkerBG;
    public GameObject orderMarker;
    public Sprite moneySprite;
    public FoodItem[] possibleItems;

    private SeatFinder seatMgr;
    private AICharacterControl charaController;
    private FoodItem chosenOrder = null;
    private const float EATING_TIME = 2.0f;
    private float currentTime = 0.0f;
    private CustomerState currentState;

    private Dictionary<CustomerState, Action<GameObject>> customerFunctions;

    void Start()
    {
        currentState = CustomerState.WALKING;

        customerFunctions = new Dictionary<CustomerState, Action<GameObject>>();
        customerFunctions.Add(CustomerState.WAITING_FOR_ORDER, GetFoodFromPlayer);
        customerFunctions.Add(CustomerState.WAITING_TO_ORDER, ShowOrder);
        customerFunctions.Add(CustomerState.WAITING_TO_PAY, PayForFood);

        seatMgr = UnityEngine.Object.FindObjectOfType<SeatFinder>();
        charaController = GetComponentInChildren<AICharacterControl>();

        int seatIndex = seatMgr.getUnoccupiedSeatIndex();
        GameObject seat = seatMgr.occupySeat(seatIndex);
        MoveToSeat(seat);

        StartCoroutine(StartOrdering());
    }

    IEnumerator StartOrdering()
    {
        yield return new WaitForSeconds(5);

        currentState = CustomerState.WAITING_TO_ORDER;
        orderMarkerBG.GetComponent<SpriteRenderer>().enabled = true;
    }

    void MoveToSeat(GameObject seat)
    {
        charaController.SetTarget(seat.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == CustomerState.EATING)
        {
            currentTime = currentTime + Time.deltaTime;
            if (currentTime >= EATING_TIME)
            {
                currentTime = 0.0f;
                orderMarkerBG.SetActive(true);
                orderMarker.SetActive(true);
                orderMarker.GetComponent<SpriteRenderer>().sprite = moneySprite;
                currentState = CustomerState.WAITING_TO_PAY;
            }
        }
    }

    private void ShowOrder(GameObject player)
    {
        int chosenIndex = Mathf.CeilToInt(UnityEngine.Random.value * possibleItems.Length) - 1;
        chosenOrder = possibleItems[chosenIndex];
        orderMarker.GetComponent<SpriteRenderer>().sprite = chosenOrder.Icon;
        orderMarker.SetActive(true);
        currentState = CustomerState.WAITING_FOR_ORDER;
    }

    private void StartEating()
    {
        currentState = CustomerState.EATING;
        orderMarker.SetActive(false);
        orderMarkerBG.SetActive(false);
    }

    private void GetFoodFromPlayer(GameObject player)
    {
        PlayerInventory inventory = player.GetComponent<PlayerInventory>();
        if (inventory.DoesPlayerHave(chosenOrder))
        {
            inventory.EmptyInventory();
            StartEating();
        }
    }

    private void PayForFood(GameObject player)
    {
        player.GetComponent<PlayerInventory>().AddMoney(chosenOrder.ItemPrice);
        currentState = CustomerState.LEAVING;
        orderMarkerBG.SetActive(false);
        orderMarker.SetActive(false);
        charaController.SetTarget(GameObject.FindWithTag("CustomerSpawn").transform);
    }

    public override void TriggeredByPlayer(GameObject player)
    {
        if (customerFunctions.ContainsKey(currentState))
        {
            customerFunctions[currentState].Invoke(player);
        }
    }

    public bool IsLeaving()
    {
        return currentState == CustomerState.LEAVING;
    }
}
