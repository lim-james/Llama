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
    private bool holding;
    private int itemCount;

    private int _selectedIndex;
    public int SelectedIndex
    {
        get { return _selectedIndex; }
        set
        {
            if (value < 0)
                _selectedIndex = slots.Count - 1;
            else if (value >= slots.Count)
                _selectedIndex = 0;
            else
                _selectedIndex = value;

            selected.position = slots[SelectedIndex].transform.position;
        }
    }

    private Dictionary<int, Fruit> inventory;

    private void Awake()
    {
        holding = true;
        SelectedIndex = 0;
        inventory = new Dictionary<int, Fruit>();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    --SelectedIndex;
        //}
        //else if (Input.GetKeyDown(KeyCode.E))
        //{
        //    ++SelectedIndex;
        //}
        
        if (holding)
            magnitude += Time.deltaTime * stats.characterStrength * stats.characterStrength; 
    }

    private void OnPickup()
    {
        Debug.Log("pick up");
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

        while (inventory.ContainsKey(SelectedIndex) && inventory[SelectedIndex] != null)
            ++SelectedIndex;

        slots[SelectedIndex].item.transform = nearestFruit.transform;
        inventory[SelectedIndex] = nearestFruit.GetComponent<Fruit>();
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

        Fruit fruit = inventory[SelectedIndex];

        if (fruit == null) return;
        --itemCount;

        fruit.transform.position = transform.position + transform.forward + new Vector3(0.0f, 1.0f, 0.0f);
        fruit.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        fruit.GetComponent<Rigidbody>().useGravity = true;
        fruit.GetComponent<Rigidbody>().velocity = transform.forward * magnitude * stats.characterStrength;
        fruit.GetComponent<Collider>().enabled = true;
        fruit.GetComponent<RangeDetector>().active = true;
        fruit.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;

        slots[SelectedIndex].item.transform = null;
        inventory[SelectedIndex] = null;
    }

}