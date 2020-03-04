using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicManager : MonoBehaviour
{
    public List<Cinematic> cameras;
    private Camera active;

    private float et;
    [SerializeField]
    private float shotTime;

    private void Awake()
    {
        cameras = new List<Cinematic>();
    }

    private void Start()
    {
        et = 0.0f;
    }

    private void FixedUpdate()
    {
        et += Time.fixedDeltaTime; 
        if (et > shotTime)
        {
            et = 0.0f;
            if (active != null) active.enabled = false;
            active = cameras[Random.Range(0, cameras.Count)].GetComponent<Camera>();
            active.enabled = true;
        }
    }
}
