using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterInfo))]
public class CharacterInventory : MonoBehaviour
{
    [SerializeField]
    private float pickupRadius = 3.0f;
    [SerializeField]
    private LayerMask pickUpObjectLayer;
    [SerializeField]
    private List<Container> slots = new List<Container>();
    [SerializeField]
    private Transform selected;
    [SerializeField]
    private Transform indicator;
    [SerializeField]
    private GameObject inventoryUI;

    [SerializeField]
    private float displacement;

    // references
    private CharacterInfo info;
    private AudioPlayer player;

    // member attributes
    private bool _holding; 
    public bool holding
    {
        get
        {
            return _holding;
        }
        set
        {
            _holding = value;

            if (holding) magnitude = 0.0f;
            bt = 0.0f;
        }
    }

    private float _magnitude;
    public float magnitude
    {
        get
        {
            return _magnitude;
        }
        private set
        {
            _magnitude = value;

            float length = magnitude * 0.25f;
            indicator.localPosition = new Vector3(0.0f, 1.0f, length * 0.5f + 4.0f);
            indicator.localScale = new Vector3(0.2f, length, 0.2f);
        }

    }

    private float holdDelay;
    private float bt;

    private int _itemCount; 
    public int itemCount
    {
        get
        {
            return _itemCount;
        }
        set
        {
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
            else if (value >= info.maxHold)
                _selectedIndex = 0;
            else
                _selectedIndex = value;

            selected.position = slots[selectedIndex].transform.position;
        }
    }

    public Dictionary<int, Fruit> inventory { get; private set; }

    private void Awake()
    {
        info = GetComponent<CharacterInfo>();
        player = GameObject.FindGameObjectWithTag("System").GetComponent<AudioPlayer>();

        holding = false;
        holdDelay = 0.4f;

        selectedIndex = 0;
        inventory = new Dictionary<int, Fruit>();
    }

    private void Start()
    {
        magnitude = 0.0f;
    }

    private void Update()
    {
        if (holding)
        {
            if (bt < holdDelay)
            {
                bt += Time.deltaTime;
            }
            else
            {
                bt += Time.deltaTime * info.strength;

                float m = magnitude + 1.0f;
                if (bt > holdDelay * m)
                {
                    magnitude = Mathf.Min(m, 3.0f);
                    bt = holdDelay;
                }
            }
        }
    }

    public void PickUpNearbyFruit()
    {
        if (itemCount == info.maxHold) return;

        Collider[] objectsNearBy = Physics.OverlapSphere(transform.position, pickupRadius, pickUpObjectLayer.value);

        float nearestDist = float.MaxValue;
        GameObject nearestFruit = null;

        for (int i = 0; i < objectsNearBy.Length; ++i)
        {
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

        gameObject.GetComponent<CharacterMovement>().GetAnimator.SetTrigger("TriggerPickup");

        nearestFruit.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        nearestFruit.GetComponent<Rigidbody>().useGravity = false;
        nearestFruit.GetComponent<Rigidbody>().velocity = Vector3.zero;
        nearestFruit.GetComponent<Collider>().enabled = false;
        nearestFruit.GetComponent<RangeDetector>().active = false;
        nearestFruit.GetComponent<Fruit>().throwing = false;
        nearestFruit.GetComponent<Fruit>().RemovePlayerBaseScore();
        nearestFruit.layer = LayerMask.NameToLayer(nearestFruit.GetComponent<Fruit>().defaultLayerMaskName);
        // TODO: Remove when all fruit now use the model gameobject as the child
        /*
        if (nearestFruit.GetComponent<MeshRenderer>()) // Change to get MeshRenderer in gameobject child when theres a fruit. 
            nearestFruit.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        else
            nearestFruit.transform.GetChild(2).GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        */

        while (inventory.ContainsKey(selectedIndex) && inventory[selectedIndex] != null)
            ++selectedIndex;

        slots[selectedIndex].item.transform = nearestFruit.transform;
        inventory.Add(selectedIndex, nearestFruit.GetComponent<Fruit>());
        ++itemCount;
    }

    public void HoldFruit()
    {
        if (!inventory.ContainsKey(selectedIndex)) return;
        holding = true;
    }

    public void ReleaseFruit()
    {
        holding = false;
        if (itemCount == 0) return;

        Fruit fruit = inventory[selectedIndex];

        if (fruit == null) return;

        gameObject.GetComponent<CharacterMovement>().GetAnimator.SetTrigger("TriggerAttack");

        fruit.transform.position = indicator.position + transform.forward * displacement + new Vector3(0.0f, 3.0f, 0.0f);
        fruit.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        fruit.transform.forward = transform.forward;
        fruit.GetComponent<Rigidbody>().useGravity = true;
        fruit.GetComponent<Rigidbody>().velocity = transform.forward * magnitude * info.strength * 10.0f;
        fruit.GetComponent<Collider>().enabled = true;
        fruit.GetComponent<RangeDetector>().active = true;
        fruit.GetComponent<Fruit>().throwing = true;
        fruit.gameObject.layer = LayerMask.NameToLayer(fruit.fruitLayerName);
        fruit.AddPlayerBaseScore();

        player.PlayThrow((int)magnitude);

        magnitude = 0.0f;

        --itemCount;
        slots[selectedIndex].item.transform = null;
        inventory.Remove(selectedIndex);

        if (itemCount > 0)
        {
            do
            {
                ++selectedIndex;
            } while (!inventory.ContainsKey(selectedIndex));
        }
    }

    public void DiscardFruits()
    {
        holding = false;
        magnitude = 0.0f;

        if (itemCount == 0) return;

        foreach (int index in inventory.Keys)
        {
            Fruit fruit = inventory[index];
            if (fruit == null) continue;
            fruit.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            fruit.transform.position = transform.position + new Vector3(0.0f, -5.0f, 0.0f);
            fruit.GetComponent<Rigidbody>().useGravity = true;
            fruit.GetComponent<Collider>().enabled = true;
            fruit.GetComponent<RangeDetector>().active = true;
            fruit.GetComponent<Fruit>().throwing = true;
            fruit.gameObject.layer = LayerMask.NameToLayer(fruit.fruitLayerName);

            slots[index].item.transform = null;
        }

        inventory.Clear();
        itemCount = 0;
    }

    public void SetInventoryUIVisibility(bool active)
    {
        inventoryUI.SetActive(active);
    }
}