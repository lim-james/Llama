using UnityEngine;

[CreateAssetMenu(fileName = "new fruit stat", menuName = "Fruit/Fruit Stats", order = 1)]
public class FruitStats : ScriptableObject
{
    [Header("Boost")]
    public float speed;
    public float strength;
    public float balance;
    public int points;
}

