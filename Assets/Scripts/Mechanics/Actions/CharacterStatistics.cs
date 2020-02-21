using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatistics : MonoBehaviour
{
    [SerializeField]
    private CharacterStats stats;

    public float speedBoost;
    public float speed
    {
        get { return stats.speed + speedBoost; }
    }

    public float strengthBoost;
    public float strength
    {
        get { return stats.strength + strengthBoost; }
    }

    public float balanceBoost;
    public float balance
    {
        get { return stats.balance + balanceBoost; }
    }

    public int maxHold
    {
        get { return stats.maxHold; }
    }


}
