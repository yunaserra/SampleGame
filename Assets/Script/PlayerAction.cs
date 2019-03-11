using UnityEngine;

public class PlayerAction : MonoBehaviour {
    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.GetComponent<Interactables>())
        {
            if (Input.GetButtonDown("Talk"))
            {
                collision.gameObject.GetComponent<Interactables>().TriggeredByPlayer(gameObject);
            }
        }
    }
}
