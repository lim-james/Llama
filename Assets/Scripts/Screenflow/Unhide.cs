using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unhide : MonoBehaviour
{
    public GameObject toHide;
    public Toggle toggle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Show()
    {
        if (toggle.isOn)
            toHide.SetActive(true);
        else
            toHide.SetActive(false);
    }
}
