using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScaling : MonoBehaviour
{
    [SerializeField]
    private float duration;
    [SerializeField]
    private float delay;
    [SerializeField]
    private Vector3 startScale;
    [SerializeField]
    private Vector3 endScale;

    private float et;

    private void Start()
    {
        et = -delay;
    }

    private void Update()
    {
        et += Time.deltaTime;

        if (et >= 0.0f && et < duration)
        {
            transform.localScale = (endScale - startScale) * et / duration + startScale;
        }
    }
}
