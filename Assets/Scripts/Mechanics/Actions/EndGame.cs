using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using UnityEngine.InputSystem;

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

    private InputMaster input;

    [SerializeField]
    private float maxHeight;
    [SerializeField]
    private float minHeight;

    [SerializeField]
    private TeamGroup teamGroup;

    [SerializeField]
    private PlayerBase[] playerBases = new PlayerBase[4];

    [SerializeField]
    private PlatformRise[] platforms = new PlatformRise[4];

    [SerializeField]
    private Text[] placements = new Text[4];
    private int[] scoresAccordingToPlayer = new int[4];

    [SerializeField]
    private GameObject Llamas;

    public bool allowRestart;

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

        for (int i = 0; i < 4; ++i)
        {
            placements[i].text = "0";
        }
    }

    private void Awake()
    {
        input = new InputMaster();
        input.Enable();

        allowRestart = false;
    }

    void Start()
    {
        timer = delayBeforeGameEnd;

        input.devices = new[] { InputDevice.all[0] };
        // restart
        
    }

    void Update()
    {
        if (!startEndGame)
            return;

        timer -= Time.deltaTime;

        if (timer <= 0 && !doOnce)
        {
            CharacterInput[] characters = GameObject.FindObjectsOfType<CharacterInput>();
            //PlayerBase[] playerBases = GameObject.FindObjectsOfType<PlayerBase>();
            //PlatformRise[] platforms = GameObject.FindObjectsOfType<PlatformRise>();

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

            // find team with highest score
            int highestScore = 0;
            int lowestScore = 10000;
            for(int i = 0; i < teamScores.Count; ++i)
            {
                if(teamScores[i].points > highestScore)
                    highestScore = teamScores[i].points;
                else if (teamScores[i].points < lowestScore)
                    lowestScore = teamScores[i].points;
            }

            float range = maxHeight - minHeight;

            //Do Podium here
            for (int i = 0; i < platforms.Length; ++i)
            {
                for (int j = 0; j < characters.Length; ++j)
                {
                    if (characters[j].GetComponent<CharacterInfo>().playerID == i)
                    {
                        // move character to their podiums
                        characters[j].transform.position = platforms[i].transform.position;
                        characters[j].transform.localEulerAngles = new Vector3(0, platforms[i].transform.localEulerAngles.y, 0);

                        //Disable Character Inventory UI
                        characters[j].GetComponent<CharacterInventory>().SetInventoryUIVisibility(false);

                        // change podium colors
                        platforms[i].team = characters[j].GetComponent<CharacterInfo>().team;
                        
                        break;
                    }
                }

                for (int j = 0; j < teamScores.Count; ++j)
                {
                    // (score - lowestScore) / (highest score - lowestScore) * (max height - min height)
                    if (platforms[i].team == teamScores[j].teamName)
                    {
                        // scores based on team
                        platforms[i].testPoints = (((teamScores[j].points - (float)lowestScore) / ((float)highestScore - (float)lowestScore)) * range) + minHeight;
                        placements[i].text = (j + 1).ToString();
                        break;
                    }
                }

                platforms[i].startMove = true;
            }

            // placements
            //for(int i = 0; i < teamScores.Count; ++i)
            //{
            //    for(int j = 0; j < platforms.Length; ++j)
            //    {
            //        if(platforms[j].team == teamScores[i].teamName)
            //        {
            //            placements[j].text = i.ToString();
            //        }
            //    }
            //}

            //for (int i = 0; i < 4; ++i)
            //{
            //    scoresAccordingToPlayer[playerBases[i].playerID] = playerBases[i].fruitCount;
            //}
            //for (int i = 0; i < 4; ++i)
            //{
            //    if (scoresAccordingToPlayer[i] == highestScore)
            //        placements[i].text = "1";
            //    else if (scoresAccordingToPlayer[i] == lowestScore)
            //        placements[i].text = "4";
            //    else
            //    {
            //        for (int j = i + 1; j < 4; ++j)
            //        {
            //            if (scoresAccordingToPlayer[j] != highestScore && scoresAccordingToPlayer[j] != lowestScore)
            //            {
            //                if (scoresAccordingToPlayer[i] > scoresAccordingToPlayer[j])
            //                {
            //                    placements[i].text = "2";
            //                    placements[j].text = "3";
            //                }
            //                else
            //                {
            //                    placements[i].text = "3";
            //                    placements[j].text = "2";
            //                }
            //            }
            //        }
            //    }
            //}

            //input.Player.Restart.performed += context => OnRestart(context);

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

        // show text
        allowRestart = true;
        for (int i = 0; i < platforms.Length; ++i)
        {
            if (platforms[i].transform.localScale.y >= maxHeight)
            {
                for (int k = 0; k < placements.Length; ++k)
                {
                    placements[k].gameObject.SetActive(true);
                }
            }
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
