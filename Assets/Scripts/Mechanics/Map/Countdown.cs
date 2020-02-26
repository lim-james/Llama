using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    //private Time;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            if (Time.timeSinceLevelLoad >= 15 && Time.timeSinceLevelLoad < 16)
            GetComponent<Text>().text = "5";
        else if (Time.timeSinceLevelLoad >= 16 && Time.timeSinceLevelLoad < 17)
            GetComponent<Text>().text = "4";
        else if (Time.timeSinceLevelLoad >= 17 && Time.timeSinceLevelLoad < 18)
            GetComponent<Text>().text = "3";
        else if (Time.timeSinceLevelLoad >= 18 && Time.timeSinceLevelLoad < 19)
            GetComponent<Text>().text = "2";
        else if (Time.timeSinceLevelLoad >= 19 && Time.timeSinceLevelLoad < 20)
            GetComponent<Text>().text = "1";
        else if (Time.timeSinceLevelLoad >= 20 && Time.timeSinceLevelLoad < 21)
            GetComponent<Text>().text = "GO";
        else if (Time.timeSinceLevelLoad >= 21)
            gameObject.active = false;
    }
}
