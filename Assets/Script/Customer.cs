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
    public GameObject OrderMarkerBG;
    public GameObject OrderMarker;
    public Sprite MoneySprite;
    public FoodItem[] PossibleItems;

    private SeatFinder seatMgr;
    private AICharacterControl charaController;
    private FoodItem chosenOrder = null;
    private const float EATING_TIME = 2.0f;
    private float currentTime = 0.0f;
    private CustomerState currentState;
    private Dictionary<CustomerState, Action<GameObject>> customerFunctions;
    private Transform exitLocation;

    void Start()
    {
        currentState = CustomerState.WALKING;
        exitLocation = GameObject.FindWithTag("CustomerSpawn").transform;

        customerFunctions = new Dictionary<CustomerState, Action<GameObject>>();
        customerFunctions.Add(CustomerState.WAITING_FOR_ORDER, getFoodFromPlayer);
        customerFunctions.Add(CustomerState.WAITING_TO_ORDER, showOrder);
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
        OrderMarkerBG.GetComponent<SpriteRenderer>().enabled = true;
    }

    void MoveToSeat(GameObject seat)
    {
        charaController.SetTarget(seat.transform);
    }
    
    void Update()
    {
        if (currentState == CustomerState.EATING)
        {
            currentTime = currentTime + Time.deltaTime;
            if (currentTime >= EATING_TIME)
            {
                currentTime = 0.0f;
                OrderMarkerBG.SetActive(true);
                OrderMarker.SetActive(true);
                OrderMarker.GetComponent<SpriteRenderer>().sprite = MoneySprite;
                currentState = CustomerState.WAITING_TO_PAY;
            }
        }
    }

    private void showOrder(GameObject player)
    {
        int chosenIndex = Mathf.CeilToInt(UnityEngine.Random.value * PossibleItems.Length) - 1;
        chosenOrder = PossibleItems[chosenIndex];
        OrderMarker.GetComponent<SpriteRenderer>().sprite = chosenOrder.Icon;
        OrderMarker.SetActive(true);
        currentState = CustomerState.WAITING_FOR_ORDER;
    }

    private void startEating()
    {
        currentState = CustomerState.EATING;
        OrderMarker.SetActive(false);
        OrderMarkerBG.SetActive(false);
    }

    private void getFoodFromPlayer(GameObject player)
    {
        PlayerInventory inventory = player.GetComponent<PlayerInventory>();
        if (inventory.DoesPlayerHave(chosenOrder))
        {
            inventory.EmptyInventory();
            startEating();
        }
    }

    private void PayForFood(GameObject player)
    {
        player.GetComponent<PlayerInventory>().AddMoney(chosenOrder.ItemPrice);
        currentState = CustomerState.LEAVING;
        OrderMarkerBG.SetActive(false);
        OrderMarker.SetActive(false);
        charaController.SetTarget(exitLocation);
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
