using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class MapScaling : MonoBehaviour
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
            boxCollider.size = (endSize - startSize) * value / duration + startSize;
            mesh.material.SetFloat("_ShrinkTime", value);
        }
    }

    private BoxCollider boxCollider;
    private MeshRenderer mesh;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        mesh = GetComponent<MeshRenderer>();
    }
}
