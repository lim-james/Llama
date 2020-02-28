using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct MaterialPack
{
    public Material primary;
    public Material secondary;
}


[CreateAssetMenu(fileName = "new character", menuName = "Character/Character", order = 0)]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public GameObject characterPrefab;
    public CharacterStats stats;

    public List<MaterialPack> teamMaterials;
}