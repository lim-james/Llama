using System;
using UnityEngine;

[Serializable]
public class Team
{
    public Color color;
    public string name;

    public Team(Color _color, string _name)
    {
        color = _color;
        name = _name;
    }
}

[CreateAssetMenu(fileName = "new team group", menuName = "Team group", order = 0)]
public class TeamGroup : ScriptableObject
{
    public Team[] group = new Team[4];
}