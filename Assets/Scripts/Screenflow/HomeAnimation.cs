using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeAnimation : MonoBehaviour
{
    public RawImage right;
    public RawImage left;

    private Vector3 rightStartPos;
    private Vector3 leftStartPos;

    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        rightStartPos = right.transform.position;
        leftStartPos = left.transform.position;

        right.transform.position += new Vector3(0, 50, 0);
        left.transform.position -= new Vector3(0, 50, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(right.transform.position.y < rightStartPos.y)
        {
            right.transform.position -= new Vector3(0, Time.deltaTime, 0);
        }
        if (left.transform.position.y > leftStartPos.y)
        {
            left.transform.position += new Vector3(0, Time.deltaTime, 0);
        }
    }
}
