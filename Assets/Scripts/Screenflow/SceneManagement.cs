using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*public void NextScene (string currentScene)
    {
        switch(currentScene)
        {
            case "Splash":
                SceneManager.LoadScene("Home");
                break;
        }
    }*/

    public void NextScene ()
    {
        switch (gameObject.name)
        {
            case "Controls":
                SceneManager.LoadScene("Controls");
                break;
            case "BackToHome":
                SceneManager.LoadScene("Home");
                break;
            case "Lobbies":
                SceneManager.LoadScene("Lobby");
                break;
            //case "BackToLobbies":
            //    SceneManager.LoadScene("Lobbies");
            //    break;
                //case "Exit":
                //SceneManager.LoadScene("Lobbies");
                //break;
        }
    }
}
