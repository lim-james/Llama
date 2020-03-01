using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    [SerializeField]
    private SelectionInput[] inputs;
    [SerializeField]
    private Animator transitionAnim;

    public void StartGame()
    {
        // set player join info
        foreach (SelectionInput input in inputs)
        {
            JoinInfo info = new JoinInfo();
            info.characterType = input.characterIndex;
            info.team = input.teamIndex;
            info.isAI = !input.connected;

            input.Unbind();
            PlayerManager.playerQueue.Add(info);
        }

        StartCoroutine(LoadScene("Game"));
        //SceneManager.LoadScene("Game");
    }

    IEnumerator LoadScene(string sceneName)
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
    }
}
