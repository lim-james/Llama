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
    private Renderer map;
    [SerializeField]
    private ScoreAreaScaling bases;

    // hacky
    private List<Transform> rotate;

    private void Awake()
    {
        rotate = new List<Transform>();
    }

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

        Material playerBase = map.material;

        // spawn 
        for (int i = 0; i < players.Count; ++i)
        {
            JoinInfo joinInfo = players[i];
            Team team = teams.group[joinInfo.team];
            // set map colour
            playerBase.SetColor("_Player" + (i + 1), team.color);
            // set player info
            CharacterData data = characters.characters[joinInfo.characterType];
            // create game object accordingly
            Transform llama = Instantiate(data.characterPrefab).transform;
            // Material
            MaterialPack pack =  data.teamMaterials[joinInfo.team];
            llama.GetComponentInChildren<MaterialManager>().SetMaterialPack(pack);
            // Character info
            CharacterInfo info = llama.GetComponent<CharacterInfo>();
            info.playerID = i;
            info.team = team.name;
            info.AI = joinInfo.isAI;
            // Character position
            Vector3 position = bases.scoringArea[i].transform.position;
            position.y = 1;
            llama.position = position;
            // Add to container
            llama.parent = container;
            llama.GetComponent<CharacterInput>().moveable = false;

            if(i < 2) rotate.Add(llama);
        }

        playerQueue.Clear();
        players.Clear();
    }

    private void Update()
    {
        if (rotate != null)
        {
            foreach (Transform llama in rotate)
                llama.rotation = new Quaternion(0, 180, 0, 0);
            rotate = null;
        }
    }
}