using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("Dude");
	}

    void OnCollisionEnter(Collision collision)
    {
        foreach(ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.red);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
    }

    void OnTriggerStay(Collider collision)
    {
        //    void OnCollisionStay(Collision collision)
        //{
        //foreach (ContactPoint contact in collision.contacts)
        //{
        //    Debug.DrawRay(contact.point, contact.normal, Color.white);
        //}
        //Debug.Log("onTRIGGER STAY?");
        if (Input.GetButtonDown("Talk"))
        {
            Debug.Log("AM I EVEN" + collision.gameObject.name);
        }

        if (collision.gameObject.GetComponent<Interactables>())
        {
            if (Input.GetButtonDown("Talk"))
            {
                Debug.Log("PRES TALK");
                collision.gameObject.GetComponent<Interactables>().TriggeredByPlayer(gameObject);
            }
        }
    }
}
