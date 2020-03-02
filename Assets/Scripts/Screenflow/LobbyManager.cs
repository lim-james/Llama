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
    [SerializeField]
    private Renderer scroll;

    private float timer;
    [SerializeField]
    private float startDelay = 3.0f;

    private void Start()
    {
        scroll.material.SetFloat("_Delay", startDelay);
    }

    private void FixedUpdate()
    {
        // set player join info
        foreach (SelectionInput input in inputs)
        {
            if (input.connected && !input.isHolding)
            {
                timer = 0;
                scroll.material.SetFloat("_et", timer);
                return;
            }
        }

        timer += Time.fixedDeltaTime;
        scroll.material.SetFloat("_et", timer);

        if (timer >= startDelay)
            StartGame();
    }

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

        StartCoroutine(LoadScene("Loading"));
        //SceneManager.LoadScene("Game");
    }

    IEnumerator LoadScene(string sceneName)
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
    }
}
