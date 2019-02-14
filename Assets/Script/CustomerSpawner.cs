using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour {
    public GameObject customerPrefab;
    public float minSecondsBetweenCustomer = 2.0f;
    public float maxSecondsBetweenCustomer = 5.0f;

    private bool isWaitingForSpawn = false;
    private SeatFinder seatMgr;
    private int customerAmount = 0;
    private const int MAX_CUSTOMER = 5;

    // Use this for initialization
    void Start () {
        seatMgr = UnityEngine.Object.FindObjectOfType<SeatFinder>();
    }

    IEnumerator SpawnCustomers()
    {
        isWaitingForSpawn = true;
        customerAmount++;
        yield return new WaitForSeconds(Random.Range(minSecondsBetweenCustomer, maxSecondsBetweenCustomer));
        Debug.Log("Instantiate in " + gameObject.transform.position);
        Instantiate(customerPrefab, gameObject.transform.position, Quaternion.identity);
        isWaitingForSpawn = false;
    }
	
	// Update is called once per frame
	void Update () {
		// spawn customers every <x> minute 
        // or if <amount> < capacity
        // depends on TOD, etc
        if (!isWaitingForSpawn && customerAmount < MAX_CUSTOMER)
        {
            StartCoroutine(SpawnCustomers());
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Customer" && other.gameObject.GetComponent<Customer>().IsLeaving())
        {
            Destroy(other.gameObject);
            customerAmount--;
        }
    }
}
