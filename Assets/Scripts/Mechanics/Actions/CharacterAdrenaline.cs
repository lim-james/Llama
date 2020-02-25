using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterInventory))]
public class CharacterAdrenaline : MonoBehaviour
{
    [SerializeField]
    private float boostDuration;

    private float boostTimeLeft;

    private CharacterInfo info;
    private CharacterInventory inventory;

    [SerializeField]
    private bool frenzy = false;

    [SerializeField]
    private int frenzySpeedBoost = 20;
    [SerializeField]
    private int frenzyStrengthBoost = 20;
    [SerializeField]
    private int frenzyBalanceBoost = 20;

    private void Awake()
    {
        info = GetComponent<CharacterInfo>();
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
            info.speedBoost = 0.0f;
            info.strengthBoost = 0.0f;
            info.balanceBoost = 0.0f;
        }
    }

    public void Consume()
    {
        bool boosted = false;
        inventory.itemCount = 0;

        foreach (Fruit fruit in inventory.inventory.Values)
        {
            if (fruit != null)
            {
                if (!frenzy)
                {
                    boosted = true;
                    info.speedBoost += fruit.stats.speed;
                    info.strengthBoost += fruit.stats.strength;
                    info.balanceBoost += fruit.stats.balance;
                }

                Destroy(fruit.gameObject);
            }
        }

        if (boosted) boostTimeLeft += boostDuration; 
    }

    public void ActivateFrenzy(float duration)
    {
        info.speedBoost = frenzySpeedBoost;
        info.strengthBoost = frenzyStrengthBoost;
        info.balanceBoost = frenzyBalanceBoost;

        boostTimeLeft = duration;
        frenzy = true;
    }
}
