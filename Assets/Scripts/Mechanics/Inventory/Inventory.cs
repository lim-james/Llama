using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private List<FruitData> fruits = new List<FruitData>();
    private int currentFruitsHolding;

    private void Start()
    {
    }

    public bool AddFruitIntoInventory(Fruit fruit)
    {
        if (fruits.Count == GetComponent<CharacterMovement>().stats.characterMaxFruitHold)
            return false;

        FruitData fruitData = new FruitData();
        fruitData.fruitName = fruit.fruitName;
        fruits.Add(fruitData);

        return true;
    }

    public void Consume()
    {
        //Need Server
    }

    public void DropWholeInventory()
    {
        //Need Server
        for (int i = 0; i < fruits.Count; ++i)
        {
            //WIP
        }

        fruits.Clear();
    }
}