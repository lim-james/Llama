using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new character", menuName = "Character/Character", order = 0)]
public class Character : ScriptableObject
{
    public string characterName;
    public GameObject characterPrefab;
    public CharacterStats stats;
}