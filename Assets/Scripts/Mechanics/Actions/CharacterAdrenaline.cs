using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterInventory))]
public class CharacterAdrenaline : MonoBehaviour
{
    [SerializeField]
    private float boostDuration;

    private float boostTimeLeft;

    private CharacterStatistics stats;
    private CharacterInventory inventory;

    private void Awake()
    {
        stats = GetComponent<CharacterStatistics>();
        inventory = GetComponent<CharacterInventory>();
    }

    private void Start()
    {
        boostTimeLeft = 0.0f;
    }

    private void Update()
    {
        if (boostTimeLeft > 0)
        {
            boostTimeLeft -= Time.deltaTime;
        }
        else
        {
            boostTimeLeft = 0.0f;
            stats.speedBoost = 0.0f;
            stats.strengthBoost = 0.0f;
            stats.balanceBoost = 0.0f;
        }
    }

    public void Consume()
    {
        bool boosted = false;
        foreach (Fruit fruit in inventory.inventory.Values)
        {
            if (fruit != null)
            {
                boosted = true;
                stats.speedBoost += fruit.stats.speed;
                stats.strengthBoost += fruit.stats.strength;
                stats.balanceBoost += fruit.stats.balance;
                Destroy(fruit.gameObject);
            }
        }

        if (boosted) boostTimeLeft += boostDuration; 
    }
}
