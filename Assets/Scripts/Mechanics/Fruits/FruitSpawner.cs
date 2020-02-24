using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    [SerializeField]
    private float spawnDelay;
    [SerializeField]
    private float heightOffset;
    public Vector2 size;
    public GameObject spawnEffectPrefab;

    [SerializeField]
    private bool spawn = true;
    private float spawnTime;
    private float testHeightOffset = 1000;

    public LayerMask avoidLayer;

    private void Update()
    {
        spawnTime += Time.deltaTime;
        if (spawn && spawnTime > spawnDelay)
        {
            //spawn = false;
            // spawn
            if (SpawnFruit())
                spawnTime = 0.0f;
        }
    }

    public bool SpawnFruit()
    {
        Vector3 randomPosition = Vector3.zero;
        if (!GetRandomPos(ref randomPosition)) return false;

        GameObject fruit = FruitManager.instance.SpawnRandomFruit();
        fruit.transform.position = randomPosition;
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

        return true;
    }

    public bool GetRandomPos(ref Vector3 position)
    {
        position = new Vector3(Random.Range(-size.x, size.x), transform.position.y + heightOffset + testHeightOffset, Random.Range(-size.y, size.y));
        RaycastHit hit;
        if (Physics.Raycast(position, -Vector3.up, out hit))
        {
            if ((avoidLayer.value & 1 << hit.collider.gameObject.layer) == 1 << hit.collider.gameObject.layer)
                return false;

            //Debug.Log(LayerMask.LayerToName(hit.collider.gameObject.layer));
            position.y = hit.point.y;
            return true;
        }

        return false;
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position + new Vector3(0, heightOffset, 0), new Vector3(size.x, 1, size.y));
    }
}
