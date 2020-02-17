using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackFade : MonoBehaviour
{
    public RawImage blackImage;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (blackImage.color.a >= 0.0f)
        {
            Color currColor = blackImage.color;
            currColor.a -= 1.2f * Time.deltaTime;
            blackImage.color = currColor;
        }
        else
            blackImage.enabled = false;
    }
}
