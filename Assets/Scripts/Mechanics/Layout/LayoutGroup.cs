using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new layout group", menuName = "Layout Group", order = 0)]
public class LayoutGroup : ScriptableObject
{
    public List<GameObject> group;
}
