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
    private float testHeightOffset = 1000;
    public Vector2 size;

    public GameObject spawnEffectPrefab;

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
        GameObject spawnEffect = Instantiate(spawnEffectPrefab);


        if (Random.Range(0, 100) <= 50)
        {
            fruit.GetComponent<Rigidbody>().AddForce(Vector3.up * 1000, ForceMode.Impulse);
            spawnEffect.transform.position = fruit.transform.position;
        }
        else
        {
            fruit.transform.position = new Vector3(fruit.transform.position.x, fruit.transform.position.y + heightOffset, fruit.transform.position.z);
            spawnEffect.transform.position = fruit.transform.position;
            spawnEffect.transform.localEulerAngles = new Vector3(spawnEffect.transform.localEulerAngles.x + 180.0f, spawnEffect.transform.localEulerAngles.y, spawnEffect.transform.localEulerAngles.z);
        }
    }

    public Vector3 GetRandomPos()
    {
        Vector3 randomPos = new Vector3(Random.Range(-size.x, size.x), transform.position.y + heightOffset + testHeightOffset, Random.Range(-size.y, size.y));
        RaycastHit hit;
        if (Physics.Raycast(randomPos, -Vector3.up, out hit))
        {
            randomPos.y = hit.point.y;
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
