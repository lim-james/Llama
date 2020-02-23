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

    public Text text;

    private Image[] players;

    // Start is called before the first frame update
    void Start()
    {
        players[0] = player1.GetComponent<Image>();
        players[1] = player2.GetComponent<Image>();
        players[2] = player3.GetComponent<Image>();
        players[3] = player4.GetComponent<Image>();
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
            text.text = "CHOOSE A TEAM";
        else if(red == 1 && blue == 1 && green == 1 && yellow == 1)
            text.text = "DEATHMATCH";
        else if(red == 1 || blue == 1 || green == 1 || yellow == 1)
        {
            if(red == 3 || blue == 3 || green == 3 || yellow == 3)
                text.text = "1 VS 3";
            else if(red == 2 || blue == 2 || green == 2 || yellow == 2)
                text.text = "1 VS 1 VS 2";
        }
        else
            text.text = "2 VS 2";
    }
}
