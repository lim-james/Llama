using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRise : MonoBehaviour
{
    public TeamName team;
    public int testPoints;
    public bool startMove = false;
    //int totalPoints;
    //int tempPoints;
    float scalePerPoint = 0.1f;
    float posPerPoint = 0.05f;
    private bool reached;
    private bool up;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        //totalPoints = 10;
        //tempPoints = 0;
        reached = false;
        up = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!startMove)
            return;

        if (transform.localScale.y < testPoints * scalePerPoint) //totalPoints* scalePerPoint
        {
            time += Time.deltaTime;

            transform.localPosition += new Vector3(0, posPerPoint * (Time.deltaTime * 1), 0);
            //transform.localScale += new Vector3(0, scalePerPoint * (Time.deltaTime * 3.0f), 0);

            if (!up && transform.localScale.y < 1.2f * scalePerPoint * time * 1.5f)
                transform.localScale += new Vector3(0, Time.deltaTime * 1.5f, 0);
            else
                up = true;

            if (up && transform.localScale.y > scalePerPoint * time * 1.5f)
                transform.localScale -= new Vector3(0, Time.deltaTime * 1.5f, 0);
            else
                up = false;

            if (transform.localScale.x < 1.2f && !reached)
                transform.localScale += new Vector3(Time.deltaTime * 1.5f, 0, Time.deltaTime * 1.5f);
            else
                reached = true;

            if (reached && transform.localScale.x > 1.0f)
                transform.localScale -= new Vector3(Time.deltaTime * 1.5f, 0, Time.deltaTime * 1.5f);
            else
                reached = false;
        }
    }
}
