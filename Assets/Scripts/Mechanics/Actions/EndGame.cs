using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public float delayBeforeGameEnd;
    private bool startEndGame;
    private float timer = 0.0f;

    public void StartEndGame()
    {
        startEndGame = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        timer = delayBeforeGameEnd;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            CharacterInput[] characters = GameObject.FindObjectsOfType<CharacterInput>();
            PlayerBase[] playerBases = GameObject.FindObjectsOfType<PlayerBase>();

            //Stop all character movement
            for (int i = 0; i < characters.Length; ++i)
            {
                characters[i].moveable = false;
            }

            if(CheckIfDraw(playerBases))
            {

            }
            else
            {
                int winningPlayer = -1;
                int highestPoints = int.MinValue;
                for (int i = 0; i < playerBases.Length; ++i)
                {
                    if (highestPoints < playerBases[i].fruitCount)
                    {

                    }
                }
            }
        }
    }

    public bool CheckIfDraw(PlayerBase[] playerBases)
    {
        for (int i = 0; i < playerBases.Length - 1; ++i)
        {
            if (playerBases[i].fruitCount != playerBases[i + 1].fruitCount)
                return false;
        }

        return true;
    }
}
