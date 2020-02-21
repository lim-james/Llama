using UnityEngine;

[CreateAssetMenu(fileName = "new character stat", menuName = "Character/Character Stats", order = 1)]
public class CharacterStats : ScriptableObject
{
    [Header("Stats")]
    public float speed;
    public float strength;
    public float balance;
    public int maxHold;
}