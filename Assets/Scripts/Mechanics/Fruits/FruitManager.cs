using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitManager : MonoBehaviour
{
    public static FruitManager instance;
    public FruitDatabase fruitDatabase;

    private Dictionary<string, GameObject> fruitGameObjectDic = new Dictionary<string, GameObject>();

    void Start()
    {
        if (!instance)
        {
            instance = this;
            InitializeDictationary();
            DontDestroyOnLoad(this);
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }
    }

    void InitializeDictationary()
    {
        for (int i = 0; i < fruitDatabase.fruits.Count; ++i)
        {
            fruitGameObjectDic.Add(fruitDatabase.fruits[i].fruitName, fruitDatabase.fruits[i].fruitPrefab);
        }
    }

    public GameObject SpawnFruit(string fruitName)
    {
        if (!fruitGameObjectDic.ContainsKey(fruitName))
            return null;

        return Instantiate(fruitGameObjectDic[fruitName]);
    }
}
