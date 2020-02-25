using System;
using UnityEngine;

public enum TeamName
{
    RED, BLUE, GREEN, YELLOW
};

[Serializable]
public class Team
{
    public Color color;
    public TeamName name;

    public Team(Color _color, TeamName _name)
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