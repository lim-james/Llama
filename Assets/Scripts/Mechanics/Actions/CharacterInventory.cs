using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : MonoBehaviour
{
    [SerializeField]
    private CharacterStats stats;
    [SerializeField]
    private float pickupRadius = 3.0f;

    public void PickUpNearbyFruit()
    {
        Debug.Log("called");

        Collider[] objectsNearBy = Physics.OverlapSphere(transform.position, pickupRadius);

        float nearestDist = float.MaxValue;
        GameObject nearestFruit = null;

        for (int i = 0; i < objectsNearBy.Length; ++i)
        {
            if (!objectsNearBy[i].GetComponent<Fruit>())
                continue;

            Vector3 dir = objectsNearBy[i].transform.position - transform.position;
            float dist = dir.magnitude;

            if (dist >= nearestDist)
                continue;

            //RaycastHit hit;
            //if (!Physics.Raycast(transform.position, dir, out hit))
            //    continue;

            if (Vector3.Dot(transform.forward, dir) < 0)
                continue;

            nearestFruit = objectsNearBy[i].gameObject;
            nearestDist = dist;
        }

        if (!nearestFruit)
            return;

        //Need server code here
        Destroy(nearestFruit);
    }

    public void ThrowFruit(Fruit fruit)
    {
        fruit.transform.SetParent(null);
        fruit.GetComponent<Rigidbody>().isKinematic = false;
        fruit.GetComponent<Collider>().isTrigger = false;
        fruit.GetComponent<Rigidbody>().AddForce(transform.forward * stats.characterStrength, ForceMode.Impulse);
    }
}