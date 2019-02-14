using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Seat
{
    private GameObject seatObject;
    private bool occupied;

    public Seat(GameObject obj)
    {
        occupied = false;
        seatObject = obj;
    }

    public GameObject SeatObject
    {
        get { return seatObject; }
    }

    public bool Occupied
    {
        get { return occupied; }
        set { occupied = value;  }
    }
}

public class SeatFinder : MonoBehaviour {
    private Seat[] seats;

    private void Awake()
    {
        GameObject[] seatObjects = GameObject.FindGameObjectsWithTag("Seat");
        seats = new Seat[seatObjects.Length];
        for (int i = 0; i < seats.Length; ++i)
        {
            seats[i] = new Seat(seatObjects[i]);
        }
    }

    public int getUnoccupiedSeatIndex()
    {
        List<int> indexes = new List<int>();
        for (int i=0; i < seats.Length; ++i)
        {
            if (!seats[i].Occupied)
            {
                indexes.Add(i);
            }
        }

        if (indexes.Count > 0)
        {
            int chosenIndex = Mathf.RoundToInt(Random.Range(0, indexes.Count));
            return indexes[chosenIndex];
        }

        return -1;
    }

   public GameObject occupySeat(int seatIndex)
    {
        seats[seatIndex].Occupied = true;

        return seats[seatIndex].SeatObject;
    }

    public bool HasEmptySeat()
    {
        for (int i = 0; i < seats.Length; ++i)
        {
            if (!seats[i].Occupied)
            {
                return true;
            }
        }

        return false;
    }

    void unoccupySeat(int seatIndex)
    {
        seats[seatIndex].Occupied = false;
    }
}
