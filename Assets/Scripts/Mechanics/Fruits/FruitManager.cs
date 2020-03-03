using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitManager : MonoBehaviour
{
    public static FruitManager instance;

    public FruitDatabase fruitDatabase;

    private Dictionary<string, GameObject> fruitGameObjectDic;
    private Dictionary<string, int> fruitMaxCount;
    private Dictionary<string, int> fruitCount;
    private List<int> spawnIndexes; // list of possible fruits left to spawn

    // list of all fruit rigidbody
    public List<Rigidbody> fruitRBList;

    private void Awake()
    {
        fruitGameObjectDic = new Dictionary<string, GameObject>();
        fruitMaxCount = new Dictionary<string, int>();
        fruitCount = new Dictionary<string, int>();
        spawnIndexes = new List<int>();
        fruitRBList = new List<Rigidbody>();
    }

    private void Start()
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

    private void InitializeDictationary()
    {
        for (int i = 0; i < fruitDatabase.fruits.Count; ++i)
        {
            FruitInfo fruit = fruitDatabase.fruits[i];
            string name = fruit.fruitName;
            fruitGameObjectDic.Add(name, fruit.fruitPrefab);
            fruitMaxCount.Add(name, fruit.maxCount);
            fruitCount.Add(name, 0);
            spawnIndexes.Add(i);
        }
    }

    public GameObject SpawnFruit(string fruitName)
    {
        if (spawnIndexes.Count == 0 ||
            !fruitGameObjectDic.ContainsKey(fruitName) ||
            fruitCount[fruitName] > fruitMaxCount[fruitName])
            return null;

        GameObject fruit = Instantiate(fruitGameObjectDic[fruitName]);
        fruitRBList.Add(fruit.GetComponent<Rigidbody>());

        return fruit;
    }

    public GameObject SpawnRandomFruit()
    {
        if (spawnIndexes.Count == 0) return null;

        int i = Random.Range(0, spawnIndexes.Count);
        int randomIndex = spawnIndexes[i];
        FruitInfo fruit = fruitDatabase.fruits[randomIndex];
        string name = fruit.fruitName;

        ++fruitCount[name];
        if (fruitCount[name] >= fruitMaxCount[name])
            spawnIndexes.RemoveAt(i);

        GameObject fruitObj = Instantiate(fruitDatabase.fruits[randomIndex].fruitPrefab);
        fruitRBList.Add(fruitObj.GetComponent<Rigidbody>());

        return fruitObj;
    }
}
