using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleLobby : MonoBehaviour
{
    public RawImage findLobby;
    public RawImage translucent;
    public RawImage createLobby;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToFindLobby()
    {
        findLobby.transform.SetSiblingIndex(2);
        translucent.transform.SetSiblingIndex(1);
        createLobby.transform.SetSiblingIndex(0);
    }

    public void ToCreateLobby()
    {
        findLobby.transform.SetSiblingIndex(0);
        translucent.transform.SetSiblingIndex(1);
        createLobby.transform.SetSiblingIndex(2);
    }
}
