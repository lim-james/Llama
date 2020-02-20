using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseColliderMovement : MonoBehaviour
{
    public Vector3 startSize;
    public Vector3 endSize;
    public Vector3 startPos;
    public Vector3 endPos;

    public float duration;

    private float et;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        et += Time.deltaTime;

        if (et >= 0.0f && et < duration)
        {
            transform.localScale = (endSize - startSize) * et / duration + startSize;
            transform.position = (endPos - startPos) * et / duration + startPos;
        }
    }
}
