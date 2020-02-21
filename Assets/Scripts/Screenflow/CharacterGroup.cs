using System;
using UnityEngine;

[Serializable]
public class CharacterType
{
    public GameObject characterModel;

    public CharacterType(GameObject _characterModel)
    {
        characterModel = _characterModel;
    }
}

[CreateAssetMenu(fileName = "new character group", menuName = "Character group", order = 0)]
public class CharacterGroup : ScriptableObject
{
    public CharacterType[] group = new CharacterType[4];
}