using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Versus : MonoBehaviour
{
    [SerializeField]
    private TeamGroup teams;
    [SerializeField]
    private RawImage player1;
    [SerializeField]
    private RawImage player2;
    [SerializeField]
    private RawImage player3;
    [SerializeField]
    private RawImage player4;

    private RawImage[] players = new RawImage[4];

    private Text text;
    private string previoustext;
    [SerializeField]
    private UnityEvent OnModeChange;

    private void Awake()
    {
        text = GetComponent<Text>();
    }
    
    private void Start()
    {
        players[0] = player1;
        players[1] = player2;
        players[2] = player3;
        players[3] = player4;

        previoustext = "DEATHMATCH";
    }
    
    private void Update()
    {
        ChangeText();
    }

    public void ChangeText()
    {
        // CHOOSE A TEAM, DEATHMATCH (1 VS 1 VS 1 VS 1), 1 VS 3, 2 VS 2, 1 VS 1 VS 2

        int red = 0, blue = 0, green = 0, yellow = 0;

        for(int i = 0; i < 4; ++i)
        {
            if (players[i].texture == teams.group[0].teamBackground)
                red++;
            else if (players[i].texture == teams.group[1].teamBackground)
                blue++;
            else if (players[i].texture == teams.group[2].teamBackground)
                green++;
            else if(players[i].texture == teams.group[3].teamBackground)
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

        if(previoustext != text.text)
        {
            if (OnModeChange != null) OnModeChange.Invoke();
            previoustext = text.text;
        }
    }
}
