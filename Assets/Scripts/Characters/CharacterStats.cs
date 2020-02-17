using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new character stat", menuName = "Character/Character Stats", order = 1)]
public class CharacterStats : ScriptableObject
{
    [Header("Stats")]
    public float characterSpeed;
    public int characterMaxFruitHold;
}