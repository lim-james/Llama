using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new fruit info", menuName = "Fruit/Fruit Info", order = 0)]
public class FruitInfo : ScriptableObject
{
    public string fruitName;
    public GameObject fruitPrefab;
}