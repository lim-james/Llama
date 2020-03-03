using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public bool paused;

    [SerializeField]
    private GameObject pauseObj;
    [SerializeField]
    private TimeController timeController;
    [SerializeField]
    private FruitManager fruitsManager;
    [SerializeField]
    private PlayerManager playerManager;

    [Header("Button Type")]
    [SerializeField]
    private Texture normalButton;
    [SerializeField]
    private Texture selectedButton;
    [SerializeField]
    private GameObject controlUI;
    [SerializeField]
    private int index;

    [SerializeField]
    private RawImage Controls;
    [SerializeField]
    private RawImage Exit;

    public float et;

    // Start is called before the first frame update
    void Start()
    {
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TogglePause()
    {
        if (et >= 0)
        {
            if (pauseObj.active == false)
            {
                // pause
                pauseObj.SetActive(true);
                paused = true;

                for (int i = 0; i < fruitsManager.fruitRBList.Count; ++i)
                {
                    fruitsManager.fruitRBList[i].constraints = RigidbodyConstraints.FreezePosition;
                }
                for (int i = 0; i < playerManager.playerRBList.Count; ++i)
                {
                    playerManager.playerList[i].GetComponent<CharacterMovement>().canMove = false;
                }
            }
            else
            {
                pauseObj.SetActive(false);
                paused = false;
                controlUI.active = false;

                for (int i = 0; i < fruitsManager.fruitRBList.Count; ++i)
                {
                    fruitsManager.fruitRBList[i].constraints = RigidbodyConstraints.None;
                }
                for (int i = 0; i < playerManager.playerRBList.Count; ++i)
                {
                    playerManager.playerList[i].GetComponent<CharacterMovement>().canMove = true;
                }
            }

            timeController.TogglePaused();
        }
    }

    public void ToggleButton(float context)
    {
        if (controlUI.active == false)
            index += (int)context;

        if (index < 0)
            index = 0;
        else if (index > 1)
            index = 1;

        switch (index)
        {
            case 0:
                Controls.texture = selectedButton;
                Exit.texture = normalButton;
                break;
            case 1:
                Controls.texture = normalButton;
                Exit.texture = selectedButton;
                break;
        }

        // Play Audio
        //player.Play(switchAudio);
    }
    public void Enter()
    {
        // Play Audio
        //player.Play(normalAudio);

        if (Controls.texture == selectedButton)
        {
            if (controlUI.active == false)
                controlUI.active = true;
            else
                controlUI.active = false;
        }
        else if (Exit.texture == selectedButton)
            SceneManager.LoadScene("Lobby");
    }
}
