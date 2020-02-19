using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScaling : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(-speed * Time.deltaTime, -speed * Time.deltaTime, 0);
    }
}
