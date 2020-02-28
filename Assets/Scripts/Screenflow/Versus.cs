using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Versus : MonoBehaviour
{
    [SerializeField]
    private TeamGroup teams;
    //[SerializeField]
    //private Image player1;
    //[SerializeField]
    //private Image player2;
    //[SerializeField]
    //private Image player3;
    //[SerializeField]
    //private Image player4;
    [SerializeField]
    private RawImage player1;
    [SerializeField]
    private RawImage player2;
    [SerializeField]
    private RawImage player3;
    [SerializeField]
    private RawImage player4;

    private RawImage[] players = new RawImage[4];

    // animation
    private bool animate;
    private bool large;
    private Vector3 originalScale;
    private string previoustext;
    [SerializeField]
    private RawImage background;

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
        animate = false;
        large = false;
        originalScale = new Vector3(1, 1, 1);
        previoustext = "DEATHMATCH";
    }

    // Update is called once per frame
    void Update()
    {
        ChangeText();

        if (animate)
        {
            if (!large && transform.localScale.x < originalScale.x * 1.3)
            {
                transform.localScale += new Vector3(Time.deltaTime * 2, Time.deltaTime * 2, Time.deltaTime * 2);
                background.transform.localScale += new Vector3(Time.deltaTime * 2, Time.deltaTime * 2, Time.deltaTime * 2); ;
            }
            else if (!large && transform.localScale.x > originalScale.x * 1.3)
                large = true;
        
            if(large && transform.localScale.x > originalScale.x)
            {
                transform.localScale -= new Vector3(Time.deltaTime * 2, Time.deltaTime * 2, Time.deltaTime * 2);
                background.transform.localScale -= new Vector3(Time.deltaTime * 2, Time.deltaTime * 2, Time.deltaTime * 2);
            }
            else if(large && transform.localScale.x < originalScale.x)
            {
                animate = false;
                large = false;
            }
        }
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

        if(previoustext != gameObject.GetComponent<Text>().text)
        {
            animate = true;
            previoustext = gameObject.GetComponent<Text>().text;
        }
    }
}
