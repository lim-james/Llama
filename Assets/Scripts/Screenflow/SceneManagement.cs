using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{

    public Animator transitionAnim;
    
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
                //SceneManager.LoadScene("Controls");
                SceneManager.LoadScene("Controls");
                //StartCoroutine(LoadScene("Controls"));
                break;
            case "BackToHome":
                //SceneManager.LoadScene("Home");
                StartCoroutine(LoadScene("Home"));
                break;
            case "Lobbies":
                //SceneManager.LoadScene("Lobby");
                StartCoroutine(LoadScene("Lobby"));
                break;
            case "Quit":
#if UNITY_STANDALONE
                Application.Quit();
#endif
#if UNITY_EDITOR
                Debug.Log("Exit from editor");
                UnityEditor.EditorApplication.isPlaying = false;
#endif
                break;
            //case "BackToLobbies":
            //    SceneManager.LoadScene("Lobbies");
            //    break;
                //case "Exit":
                //SceneManager.LoadScene("Lobbies");
                //break;
        }
    }

    IEnumerator LoadScene(string sceneName)
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
    }
}
