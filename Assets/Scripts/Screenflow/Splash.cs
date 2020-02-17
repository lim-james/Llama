using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Splash : MonoBehaviour
{
    float timer;
    public RawImage blackImage;

    // Start is called before the first frame update
    void Start()
    {
        timer = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0.0f)
        {
            Color currColor = blackImage.color;
            currColor.a += 1.2f * Time.deltaTime;
            blackImage.color = currColor;
        }

        if (blackImage.color.a >= 1.0f)
            SceneManager.LoadScene("Home");
        //gameObject.GetComponent<SceneManagement>().NextScene("Splash");
    }
}
