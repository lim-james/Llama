using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public float delayBeforeGameEnd;
    private bool startEndGame = false;
    private float timer = 0.0f;

    public Text timerText;
    public string endText;

    public void StartEndGame()
    {
        startEndGame = true;
        timerText.enabled = true;
    }

    void Start()
    {
        timer = delayBeforeGameEnd;
    }

    void Update()
    {
        if (startEndGame)
            return;

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

            timerText.text = endText;
        }
        else
        {
            timerText.text = Mathf.CeilToInt(timer).ToString();
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
