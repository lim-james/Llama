using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterInventory))]
public class CharacterAdrenaline : MonoBehaviour
{

    private CharacterStats stats;
    private CharacterInventory inventory;

    private void Awake()
    {
        inventory = GetComponent<CharacterInventory>();
    }

    private void Start()
    {
        stats = GetComponent<CharacterStatistics>().stats;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Consume()
    {

    }
}
