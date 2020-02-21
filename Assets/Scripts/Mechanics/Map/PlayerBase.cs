using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public int playerID;
    public int fruitCount;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<Fruit>())
            return;

        fruitCount += other.GetComponent<Fruit>().stats.points;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.GetComponent<Fruit>())
            return;

        fruitCount -= other.GetComponent<Fruit>().stats.points;
    }
}
