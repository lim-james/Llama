using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    [SerializeField]
    private float spawnDelay;
    private float spawnTime;

    private bool spawn = true;

    public float heightOffset;
    public Vector2 size;

    private void Awake()
    {
    }

    private void Update()
    {
        spawnTime += Time.deltaTime;
        if (spawn && spawnTime > spawnDelay)
        {
            //spawn = false;
            // spawn
            SpawnFruit();
            spawnTime = 0.0f;
        }
    }

    public void SpawnFruit()
    {
        GameObject fruit = FruitManager.instance.SpawnRandomFruit();
        fruit.transform.position = GetRandomPos();
    }

    public Vector3 GetRandomPos()
    {
        Vector3 randomPos = new Vector3(Random.Range(-size.x, size.x), transform.position.y + heightOffset, Random.Range(-size.y, size.y));
        if (Physics.Raycast(randomPos, -Vector3.up))
        {
            return randomPos;
        }

        return GetRandomPos();
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(Color.blue.r, Color.blue.g, Color.blue.b, 0.2f);
        Gizmos.DrawWireCube(transform.position + new Vector3(0, heightOffset, 0), new Vector3(size.x, 1, size.y));
    }
}
