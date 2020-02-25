using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class JoinInfo
{
    public int characterType;
    public int team;
    public bool isAI;
}

public class PlayerManager : MonoBehaviour
{
    public static List<JoinInfo> playerQueue = new List<JoinInfo>();

    [Header("Players")]
    [SerializeField]
    private List<JoinInfo> players;
    [SerializeField]
    private Transform container;

    [Header("Game data")]
    [SerializeField]
    private CharactersDatabase characters;
    [SerializeField]
    private TeamGroup teams;

    [Header("References")]
    [SerializeField]
    private ScoreAreaScaling bases;

    private void Start()
    {
        // exception handling
        if (players == null || players.Count == 0)
        {
            if (playerQueue.Count == 0)
            {
                Debug.LogError("Yo where the players at.");
                return;
            }
            else
            {
                players = playerQueue;
            }
        }

        // spawn 
        for (int i = 0; i < players.Count; ++i)
        {
            JoinInfo joinInfo = players[i];
            // create game object accordingly
            Transform llama = Instantiate(characters.characters[joinInfo.characterType].characterPrefab).transform;
            // Set character info
            CharacterInfo info = llama.GetComponent<CharacterInfo>();
            info.playerID = i;
            info.team = teams.group[joinInfo.team].name;
            info.AI = joinInfo.isAI;
            // Set character position
            Vector3 position = bases.scoringArea[i].transform.position;
            position.y = 1;
            llama.position = position;
            // add to container
            llama.parent = container;
        }
        
    }

}
