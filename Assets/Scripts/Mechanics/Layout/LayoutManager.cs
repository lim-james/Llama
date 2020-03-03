using System.Collections.Generic;
using UnityEngine;

public class LayoutManager : MonoBehaviour
{
    [SerializeField]
    private LayoutGroup layouts;
    [SerializeField]
    private Transform container;
    [SerializeField]
    private List<Transform> spawnPoints;

    private List<int> indexes;

    private void Awake()
    {
        indexes = new List<int>();
    }

    private void Start()
    {
        PopulateIndexes();

        foreach (Transform point in spawnPoints)
        {
            Transform layout = Instantiate(GetRandomLayout()).transform;
            Vector3 position = point.position;
            position.y = 0.0f;
            layout.position = position;
            layout.localEulerAngles = point.localEulerAngles;
            layout.parent = container;
        }
    }

    // helper methods

    private void PopulateIndexes()
    {
        for (int i = 0; i < layouts.group.Count; ++i)
            indexes.Add(i);
    }

    private int GetRandomIndex()
    {
        if (indexes.Count == 0)
            PopulateIndexes();

        int i = Random.Range(0, indexes.Count);
        int index = indexes[i];
        indexes.RemoveAt(i);
        return index;
    }
    
    private GameObject GetRandomLayout()
    {
        return layouts.group[GetRandomIndex()];
    }

}
