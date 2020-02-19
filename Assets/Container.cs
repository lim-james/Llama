using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public Transform transform = null;
    public Vector3 offset = Vector3.zero;
} 

public class Container : MonoBehaviour
{
    public Item item;

    private void Awake()
    {
        item = new Item();
    }

    private void Update()
    {
        if (item.transform == null) return;
        item.transform.position = Vector3.Lerp(item.transform.position, transform.position + item.offset, Time.deltaTime * 10.0f);
    }
}
