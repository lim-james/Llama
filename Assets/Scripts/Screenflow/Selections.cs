using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selections : MonoBehaviour
{
    public Texture RedTeam;
    public Texture BlueTeam;
    public Texture GreenTeam;
    //public Texture2D YellowTeam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchTeams()
    {
        //gameObject.GetComponent<RawImage>().texture. = RedTeam;

        switch (gameObject.GetComponent<RawImage>().texture.name)
        {
            case "redBox":
                gameObject.GetComponent<RawImage>().texture = BlueTeam;
                break;
            case "blueBox":
                gameObject.GetComponent<RawImage>().texture = GreenTeam;
                break;
            case "greenBox":
                gameObject.GetComponent<RawImage>().texture = RedTeam; // = YellowTeam;
                break;
            //case "yellowBox":
            //    gameObject.GetComponent<RawImage>().texture = RedTeam;
            //    break;
        }
    }
}
