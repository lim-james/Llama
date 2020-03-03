using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MaterialManager : MonoBehaviour
{
    [SerializeField]
    private List<Renderer> primary;
    [SerializeField]
    private List<Renderer> secondary;
    [SerializeField]
    private MaterialPack currentPack;

    public Material currentPrimary { get; private set; }
    public Material currentSecondary { get; private set; }

    private void Start()
    {
        SetMaterialPack(currentPack);
    }
    
    // helper methods
    public void SetMaterialPack(MaterialPack pack)
    {
        currentPack = pack;

        currentPrimary = Instantiate(pack.primary);

        foreach (Renderer r in primary)
            r.material = currentPrimary;

        if (pack.secondary != null)
        {
            currentSecondary = Instantiate(pack.secondary);
            foreach (Renderer r in secondary)
                r.material = currentSecondary;
        }
    }

}
