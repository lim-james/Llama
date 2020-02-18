using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{

    private Transform camera;
    private Quaternion rotation;

    private void Start()
    {
        camera = Camera.main.transform;
        rotation = transform.rotation;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.rotation = camera.rotation * rotation;
    }
}
