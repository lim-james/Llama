using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    [SerializeField]
    private SelectionInput[] inputs;

    public void StartGame()
    {
        // set player join info
        foreach (SelectionInput input in inputs)
        {
            JoinInfo info = new JoinInfo();
            info.characterType = input.characterIndex;
            info.team = input.index;
            info.isAI = input.connected;
            PlayerManager.playerQueue.Add(info);
        }

        SceneManager.LoadScene("Game");
    }
}
