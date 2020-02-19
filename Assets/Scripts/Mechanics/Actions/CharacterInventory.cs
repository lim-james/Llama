using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : MonoBehaviour
{
    [SerializeField]
    private CharacterStats stats;
    [SerializeField]
    private float pickupRadius = 3.0f;
    [SerializeField]
    private List<Container> slots = new List<Container>();
    [SerializeField]
    private Transform selected;

    private float magnitude;
    private int selectedIndex;
    private int itemCount;
    private Dictionary<int, Fruit> inventory;

    private void Awake()
    {
        inventory = new Dictionary<int, Fruit>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            --selectedIndex;
            if (selectedIndex < 0)
                selectedIndex = slots.Count - 1;
            selected.position = slots[selectedIndex].transform.position;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            ++selectedIndex;
            if (selectedIndex == slots.Count)
                selectedIndex = 0;
            selected.position = slots[selectedIndex].transform.position;
        }
        else if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            magnitude = 0.0f;
        }
        else if (Input.GetKey(KeyCode.LeftAlt))
        {
            magnitude += Time.deltaTime * stats.characterStrength * stats.characterStrength;
        }
        else if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            DropFruit();
        }
    }

    public void PickUpNearbyFruit()
    {
        if (itemCount == stats.characterMaxFruitHold) return;

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

            if (Vector3.Dot(transform.forward, dir) < 0)
                continue;

            nearestFruit = objectsNearBy[i].gameObject;
            nearestDist = dist;
        }

        if (!nearestFruit)
            return;

        nearestFruit.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        nearestFruit.GetComponent<Rigidbody>().useGravity = false;
        nearestFruit.GetComponent<Rigidbody>().velocity = Vector3.zero;
        nearestFruit.GetComponent<Collider>().enabled = false;
        nearestFruit.GetComponent<RangeDetector>().active = false;
        nearestFruit.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        slots[selectedIndex].item.transform = nearestFruit.transform;
        inventory[selectedIndex] = nearestFruit.GetComponent<Fruit>();
        ++itemCount;
    }

    public void DropFruit()
    {
        if (itemCount == 0) return;

        Fruit fruit = inventory[selectedIndex];

        if (fruit == null) return;
        --itemCount;

        fruit.transform.position = transform.position + transform.forward + new Vector3(0.0f, 1.0f, 0.0f);
        fruit.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        fruit.GetComponent<Rigidbody>().useGravity = true;
        fruit.GetComponent<Rigidbody>().velocity = transform.forward * magnitude * stats.characterStrength;
        fruit.GetComponent<Collider>().enabled = true;
        fruit.GetComponent<RangeDetector>().active = true;
        fruit.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;

        slots[selectedIndex].item.transform = null;

        inventory[selectedIndex] = null;
    }
}