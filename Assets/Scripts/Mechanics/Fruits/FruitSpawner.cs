using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    [SerializeField]
    private float spawnDelay;
    private float spawnTime;

    private bool spawn = true;

    private void Awake()
    {
    }

    private void Update()
    {
        spawnTime += Time.deltaTime;
        if (spawn && spawnTime > spawnDelay)
        {
            spawn = false;
            //spawnTime = 0.0f;
            // spawn
        }
    }
}
