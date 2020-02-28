using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class CharacterType
{
    public CharacterData character;
    public GameObject model;
    public Vector3 modelScale;
    public Texture strength;
    public Texture speed;
    public Texture weight;
    public Texture balance;

    //public CharacterType(GameObject _characterModel)
    //{
    //    characterModel = _characterModel;
    //}
}

[CreateAssetMenu(fileName = "new character group", menuName = "Character group", order = 0)]
public class CharacterGroup : ScriptableObject
{
    public CharacterType[] group = new CharacterType[4];
}