﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class EndGame : MonoBehaviour
{
    public float delayBeforeGameEnd;
    private bool startEndGame = false;
    private float timer = 0.0f;

    public Text timerText;
    public string endText;

    public struct TeamScore
    {
        public int points;
        public TeamName teamName;
    };

    public void StartEndGame()
    {
        startEndGame = true;
        timerText.gameObject.SetActive(true);

        CharacterAdrenaline[] characterAdrenalines = GameObject.FindObjectsOfType<CharacterAdrenaline>();
        for (int i = 0; i < characterAdrenalines.Length; ++i)
        {
            characterAdrenalines[i].ActivateFrenzy(delayBeforeGameEnd);
        }
    }

    void Start()
    {
        timer = delayBeforeGameEnd;
    }

    void Update()
    {
        if (!startEndGame)
            return;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            CharacterInput[] characters = GameObject.FindObjectsOfType<CharacterInput>();
            PlayerBase[] playerBases = GameObject.FindObjectsOfType<PlayerBase>();

            timerText.text = endText;

            //Stop all character movement
            for (int i = 0; i < characters.Length; ++i)
            {
                characters[i].moveable = false;
            }

            //Get All points
            int[] points = new int[sizeof(TeamName)];
            for (int i = 0; i < playerBases.Length; ++i)
            {
                int playerID = playerBases[i].playerID;
                for (int j = 0; i < characters.Length; ++j)
                {
                    if (characters[j].GetComponent<CharacterInfo>().playerID != playerID)
                        continue;

                    points[(int)characters[j].GetComponent<CharacterInfo>().team] += playerBases[i].fruitCount;
                    break;
                }
            }

            //Check if it is a draw situation
            if (CheckIfDraw(points))
            {
                //Do Pondium Here
                return;
            }

            //Sort the score
            List<TeamScore> teamScores = GetPlacement(points);
            //Do Pondium here
        }
        else
        {
            timerText.text = Mathf.CeilToInt(timer).ToString();
        }
    }

    public bool CheckIfDraw(int[] points)
    {
        for (int i = 0; i < points.Length - 1; ++i)
        {
            if (points[i] != points[i + 1])
                return false;
        }

        return true;
    }

    public List<TeamScore> GetPlacement(int[] points)
    {
        List<TeamScore> teamScores = new List<TeamScore>();
        for (int i = 0; i < points.Length; ++i)
        {
            TeamScore teamScore = new TeamScore();
            teamScore.points = points[i];
            teamScore.teamName = (TeamName)i;
            teamScores.Add(teamScore);
        }

        return (teamScores.OrderByDescending(x => x.points).ToList());
    }
}
