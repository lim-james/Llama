using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    [SerializeField]
    private List<Renderer> primary;
    [SerializeField]
    private List<Renderer> secondary;
    [SerializeField]
    private MaterialPack currentPack;

    private void Start()
    {
        SetMaterialPack(currentPack);
    }
    
    // helper methods
    public void SetMaterialPack(MaterialPack pack)
    {
        currentPack = pack;

        foreach (Renderer r in primary)
            r.material = pack.primary;

        if (pack.secondary != null)
            foreach (Renderer r in secondary)
                r.material = pack.secondary;
    }


}
