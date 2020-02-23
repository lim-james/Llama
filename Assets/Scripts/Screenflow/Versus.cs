using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Versus : MonoBehaviour
{
    [SerializeField]
    private TeamGroup teams;
    [SerializeField]
    private Image player1;
    [SerializeField]
    private Image player2;
    [SerializeField]
    private Image player3;
    [SerializeField]
    private Image player4;

    private Image[] players = new Image[4];

    private void Awake()
    {
        players[0] = player1;
        players[1] = player2;
        players[2] = player3;
        players[3] = player4;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChangeText();
    }

    public void ChangeText()
    {
        // CHOOSE A TEAM, DEATHMATCH (1 VS 1 VS 1 VS 1), 1 VS 3, 2 VS 2, 1 VS 1 VS 2

        int red = 0, blue = 0, green = 0, yellow = 0;

        for(int i = 0; i < 4; ++i)
        {
            if (players[i].color == teams.group[0].color)
                red++;
            else if (players[i].color == teams.group[1].color)
                blue++;
            else if (players[i].color == teams.group[2].color)
                green++;
            else if(players[i].color == teams.group[3].color)
                yellow++;
        }

        if (red == 4 || blue == 4 || green == 4 || yellow == 4)
            gameObject.GetComponent<Text>().text = "CHOOSE A TEAM";
        else if(red == 1 && blue == 1 && green == 1 && yellow == 1)
            gameObject.GetComponent<Text>().text = "DEATHMATCH";
        else if(red == 1 || blue == 1 || green == 1 || yellow == 1)
        {
            if(red == 3 || blue == 3 || green == 3 || yellow == 3)
                gameObject.GetComponent<Text>().text = "1 VS 3";
            else if(red == 2 || blue == 2 || green == 2 || yellow == 2)
                gameObject.GetComponent<Text>().text = "1 VS 1 VS 2";
        }
        else
            gameObject.GetComponent<Text>().text = "2 VS 2";
    }
}
