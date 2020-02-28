using System.Collections;
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
    public Camera endGameCamera;

    public GameObject frenzyHue;
    public GameObject Shockwave;
    public GameObject[] fireworks;
    public Transform[] fireworksLocation;
    private bool doOnce = false;

    public struct TeamScore
    {
        public int points;
        public TeamName teamName;
    };

    public void StartEndGame()
    {
        startEndGame = true;
        timerText.gameObject.SetActive(true);

        Instantiate(Shockwave,new Vector3(0,1,0),new Quaternion(0,0,0,0));
        frenzyHue.SetActive(true);

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

        if (timer <= 0 && !doOnce)
        {
            CharacterInput[] characters = GameObject.FindObjectsOfType<CharacterInput>();
            PlayerBase[] playerBases = GameObject.FindObjectsOfType<PlayerBase>();
            PlatformRise[] platforms = GameObject.FindObjectsOfType<PlatformRise>();

            timerText.text = endText;

            //Stop all character movement
            for (int i = 0; i < characters.Length; ++i)
            {
                characters[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
                characters[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
                characters[i].moveable = false;
            }

            //Get All points
            int[] points = new int[sizeof(TeamName)];
            for (int i = 0; i < playerBases.Length; ++i)
            {
                int playerID = playerBases[i].playerID;
                for (int j = 0; j < characters.Length; ++j)
                {
                    if (characters[j].GetComponent<CharacterInfo>().playerID != playerID)
                        continue;

                    points[(int)characters[j].GetComponent<CharacterInfo>().team] += playerBases[i].fruitCount;
                    break;
                }
            }

            //Check if it is a draw situation
            /*
            if (CheckIfDraw(points))
            {
                //Do Pondium Here
                return;
            }
            */

            //Sort the score
            List<TeamScore> teamScores = GetPlacement(points);

            // find player with highest score
            float highestScore = 0; ;
            for(int i = 0; i < teamScores.Count; ++i)
            {
                if(teamScores[i].points > highestScore)
                {
                    highestScore = teamScores[i].points;
                }
            }

            //Do Pondium here
            for (int i = 0; i < platforms.Length; ++i)
            {
                for (int j = 0; j < teamScores.Count; ++j)
                {
                    if (platforms[i].team == teamScores[j].teamName)
                    {
                        if (teamScores[j].points == highestScore) // player with highest score
                            platforms[i].testPoints = 2.2f;
                        else
                            platforms[i].testPoints = (teamScores[j].points / highestScore) * 2.2f;

                        break;
                    }
                }

                for (int j = 0; j < characters.Length; ++j)
                {
                    if (characters[j].GetComponent<CharacterInfo>().team == platforms[i].team)
                    {
                        characters[j].transform.position = platforms[i].transform.position;
                        characters[j].transform.localEulerAngles = new Vector3(0, platforms[i].transform.localEulerAngles.y, 0);
                        break;
                    }
                }

                platforms[i].startMove = true;
            }

            timerText.gameObject.SetActive(false);
            Camera.main.gameObject.SetActive(false);
            endGameCamera.gameObject.SetActive(true);
            endGameCamera.tag = "MainCamera";
            doOnce = true;

            Instantiate(fireworks[0], fireworksLocation[0].position, fireworksLocation[0].rotation);
            Instantiate(fireworks[1], fireworksLocation[1].position, fireworksLocation[1].rotation);
                                 
            Instantiate(fireworks[0], fireworksLocation[2].position, fireworksLocation[2].rotation);
            Instantiate(fireworks[1], fireworksLocation[3].position, fireworksLocation[3].rotation);
                                 
            Instantiate(fireworks[0], fireworksLocation[4].position, fireworksLocation[4].rotation);
            Instantiate(fireworks[1], fireworksLocation[5].position, fireworksLocation[5].rotation);

            // deactive the hue
            frenzyHue.SetActive(false);
            
            //if()
            //{
            //    endGameCamera.transform.up += new Vector3(0, Time.deltaTime, 0);
            //}
        }
        else if(!doOnce)
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
