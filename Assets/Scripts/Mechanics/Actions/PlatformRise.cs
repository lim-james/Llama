using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRise : MonoBehaviour
{
    public int testPoints;
    int totalPoints;
    int tempPoints;
    float scalePerPoint = 0.1f;
    float posPerPoint = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        //totalPoints = 10;
        tempPoints = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // 1 point -> +2 yscale, + 1 ypos
        if (transform.localScale.y < testPoints * scalePerPoint) //totalPoints* scalePerPoint
        {
            transform.localScale += new Vector3(0, scalePerPoint * (Time.deltaTime * 3.0f), 0);
            transform.localPosition += new Vector3(0, posPerPoint * (Time.deltaTime * 3.0f), 0);
        }
    }
}
