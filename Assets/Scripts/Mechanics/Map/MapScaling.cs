using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class MapScaling : MonoBehaviour
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
    private BoxCollider boxCollider;
    private MeshRenderer mesh;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        mesh = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        et = -delay;
    }

    private void Update()
    {
        et += Time.deltaTime;
        mesh.material.SetFloat("_ShrinkTime", et);

        if (et >= 0.0f && et < duration)
        {
            boxCollider.size = (endSize - startSize) * et / duration + startSize;
        }
    }
}
