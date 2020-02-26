using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    private bool large;
    private bool done;

    // Start is called before the first frame update
    void Start()
    {
        large = false;
        done = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad >= 15 && Time.timeSinceLevelLoad < 16)
        {
            GetComponent<Text>().text = "5";
            if(!done)
            {
                transform.localPosition = new Vector3(0, 100, 0);
                done = true;
            }
            if(transform.localPosition.y > 0)
                transform.localPosition -= new Vector3(0, Time.deltaTime * 500, 0);
        }
        else if (Time.timeSinceLevelLoad >= 16 && Time.timeSinceLevelLoad < 17)
        {
            GetComponent<Text>().text = "4";
            if (done)
            {
                transform.localPosition = new Vector3(0, 100, 0);
                done = false;
            }
            if (transform.localPosition.y > 0)
                transform.localPosition -= new Vector3(0, Time.deltaTime * 500, 0);
        }
        else if (Time.timeSinceLevelLoad >= 17 && Time.timeSinceLevelLoad < 18)
        {
            GetComponent<Text>().text = "3";
            if (!done)
            {
                transform.localPosition = new Vector3(0, 100, 0);
                done = true;
            }
            if (transform.localPosition.y > 0)
                transform.localPosition -= new Vector3(0, Time.deltaTime * 500, 0);
        }
        else if (Time.timeSinceLevelLoad >= 18 && Time.timeSinceLevelLoad < 19)
        {
            GetComponent<Text>().text = "2";
            if (done)
            {
                transform.localPosition = new Vector3(0, 100, 0);
                done = false;
            }
            if (transform.localPosition.y > 0)
                transform.localPosition -= new Vector3(0, Time.deltaTime * 500, 0);
        }
        else if (Time.timeSinceLevelLoad >= 19 && Time.timeSinceLevelLoad < 20)
        {
            GetComponent<Text>().text = "1";
            if (!done)
            {
                transform.localPosition = new Vector3(0, 100, 0);
                done = true;
            }
            if (transform.localPosition.y > 0)
                transform.localPosition -= new Vector3(0, Time.deltaTime * 500, 0);
        }
        else if (Time.timeSinceLevelLoad >= 20 && Time.timeSinceLevelLoad < 21)
        {
            GetComponent<Text>().text = "GO";
            if(!large)
            {
                GetComponent<Text>().fontSize = 400;
                large = true;
            }
            if (GetComponent<Text>().fontSize > 200)
                GetComponent<Text>().fontSize = GetComponent<Text>().fontSize - (int)(Time.deltaTime * 800);
        }
        else if (Time.timeSinceLevelLoad >= 21)
            gameObject.active = false;
    }
}
