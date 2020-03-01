using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierScript : MonoBehaviour
{
    [SerializeField]
    private Vector3 startSize;
    [SerializeField]
    private Vector3 endSize;

    public float duration { private get; set; }

    public float et
    {
        set
        {
            transform.localScale = (endSize - startSize) * value / duration + startSize;
        }
    }
}
