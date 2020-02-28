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
    public Texture teamBackground;

    public Team(Color _color, TeamName _name, Texture _teamBackground)
    {
        color = _color;
        name = _name;
        teamBackground = _teamBackground;
    }
}

[CreateAssetMenu(fileName = "new team group", menuName = "Team group", order = 0)]
public class TeamGroup : ScriptableObject
{
    public Team[] group = new Team[4];
}