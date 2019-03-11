using System.Collections;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour {
    public GameObject CustomerPrefab;
    public float MinSecondsBetweenCustomer = 2.0f;
    public float MaxSecondsBetweenCustomer = 5.0f;

    private bool isWaitingForSpawn = false;
    private SeatFinder seatMgr;
    private int customerAmount = 0;
    private const int MAX_CUSTOMER = 5;
    
    void Start ()
    {
        seatMgr = UnityEngine.Object.FindObjectOfType<SeatFinder>();
    }

    IEnumerator SpawnCustomers()
    {
        isWaitingForSpawn = true;
        customerAmount++;
        yield return new WaitForSeconds(Random.Range(MinSecondsBetweenCustomer, MaxSecondsBetweenCustomer));
        Instantiate(CustomerPrefab, gameObject.transform.position, Quaternion.identity);
        isWaitingForSpawn = false;
    }
	
	void Update ()
    {
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
