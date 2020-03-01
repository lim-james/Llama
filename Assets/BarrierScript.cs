using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierScript : MonoBehaviour
{
    [SerializeField]
    private float duration;
    [SerializeField]
    private float delay;
    [SerializeField]
    private Vector3 startSize;
    [SerializeField]
    private Vector3 endSize;

    private float et;

    private void Start()
    {
        et = -delay;
    }

    private void Update()
    {
        et += Time.deltaTime;

        if (et >= 0.0f && et < duration)
            transform.localScale = (endSize - startSize) * et / duration + startSize;
    }
}
