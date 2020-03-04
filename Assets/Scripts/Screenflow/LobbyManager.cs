using System.Collections;
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

    [Header("Sound")]
    public AudioPlayer player;

    [SerializeField]
    private AudioClip highAudio;

    [SerializeField]
    private AudioClip normalAudio;

    [SerializeField]
    private AudioClip lowAudio;

    private bool playOnce = false;
    private bool starting = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("System").GetComponent<AudioPlayer>();
        scroll.material.SetFloat("_Delay", startDelay);
    }

    private void FixedUpdate()
    {
        // set player join info
        int playerCount = 0;
        foreach (SelectionInput input in inputs)
        {
            // player
            if (input.connected)
            {
                if (!input.isHolding)
                {
                    timer -= Time.fixedDeltaTime * 2.0f;
                    timer = Mathf.Max(0.0f, timer);
                    scroll.material.SetFloat("_et", timer);
                    return;
                }
                ++playerCount;
            }
        }

        if (playerCount == 0) return;

        timer += Time.fixedDeltaTime;
        scroll.material.SetFloat("_et", timer);

        if (timer >= 1.5f && !playOnce)
        {
            playOnce = true;
            player.PlaySFX(normalAudio);
        }

        if (timer >= startDelay && !starting)
            StartGame();
    }

    public void StartGame()
    {
        starting = true;
        // set player join info
        foreach (SelectionInput input in inputs)
        {
            JoinInfo info = new JoinInfo();
            Debug.Log(input.characterIndex);
            info.characterType = input.characterIndex;
            info.team = input.teamIndex;
            info.isAI = !input.connected;

            PlayerManager.playerQueue.Add(info);
        }

        player.PlaySFX(highAudio);
        StartCoroutine(player.FadeOut(1.5f));
        StartCoroutine(LoadScene("Loading"));
        gameObject.GetComponent<Back>().Unbind();
        //SceneManager.LoadScene("Game");
    }

    private IEnumerator LoadScene(string sceneName)
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
    }
}