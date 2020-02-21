using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStatistics))]
public class CharacterInventory : MonoBehaviour
{
    [SerializeField]
    private float pickupRadius = 3.0f;
    [SerializeField]
    private List<Container> slots = new List<Container>();
    [SerializeField]
    private Transform selected;

    // references
    private CharacterStatistics stats;

    // member attributes
    private float magnitude;
    private bool holding;

    private int _itemCount; 
    public int itemCount
    {
        get
        {
            return _itemCount;
        }
        set
        {
            magnitude = 0.0f;
            holding = false;
            _itemCount = value;
        }
    }

    private int _selectedIndex;
    public int selectedIndex
    {
        get { return _selectedIndex; }
        set
        {
            if (value < 0)
                _selectedIndex = slots.Count - 1;
            else if (value >= stats.maxHold)
                _selectedIndex = 0;
            else
                _selectedIndex = value;

            selected.position = slots[selectedIndex].transform.position;
        }
    }

    public Dictionary<int, Fruit> inventory { get; private set; }

    private void Awake()
    {
        stats = GetComponent<CharacterStatistics>();
        
        holding = true;
        selectedIndex = 0;
        inventory = new Dictionary<int, Fruit>();
    }

    private void Update()
    {
        if (holding)
            magnitude += Time.deltaTime * stats.strength * stats.strength; 
    }

    public void PickUpNearbyFruit()
    {
        if (itemCount == stats.maxHold) return;

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

        while (inventory.ContainsKey(selectedIndex) && inventory[selectedIndex] != null)
            ++selectedIndex;

        slots[selectedIndex].item.transform = nearestFruit.transform;
        inventory[selectedIndex] = nearestFruit.GetComponent<Fruit>();
        ++itemCount;
    }

    public void HoldFruit()
    {
        holding = true;
        magnitude = 0.0f;
    }

    public void ReleaseFruit()
    {
        holding = false;
        if (itemCount == 0) return;

        Fruit fruit = inventory[selectedIndex];

        if (fruit == null) return;

        fruit.transform.position = transform.position + transform.forward + new Vector3(0.0f, 1.0f, 0.0f);
        fruit.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        fruit.GetComponent<Rigidbody>().useGravity = true;
        fruit.GetComponent<Rigidbody>().velocity = transform.forward * magnitude * stats.strength;
        fruit.GetComponent<Collider>().enabled = true;
        fruit.GetComponent<RangeDetector>().active = true;
        fruit.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;

        --itemCount;
        slots[selectedIndex].item.transform = null;
        inventory[selectedIndex] = null;
    }

}