using System;
using UnityEngine;

public class CharacterStatistics : MonoBehaviour
{
    [SerializeField]
    private CharacterStats stats;

    [NonSerialized]
    public float speedBoost;
    public float speed
    {
        get { return stats.speed + speedBoost; }
    }

    [NonSerialized]
    public float strengthBoost;
    public float strength
    {
        get { return stats.strength + strengthBoost; }
    }

    [NonSerialized]
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