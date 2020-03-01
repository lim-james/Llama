using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    [Header("Time attributes")]
    [SerializeField]
    private float delay;
    [SerializeField]
    private float duration;

    private float et;

    [Header("References")]
    [SerializeField]
    private MapScaling mainMapScaling;
    [SerializeField]
    private MapScaling endMapScaling;
    [SerializeField]
    private CameraFollow mainCameraFollow;
    [SerializeField]
    private CameraFollow cinemachineCameraFollow;
    [SerializeField]
    private BarrierScript barrierScript;
    [SerializeField]
    private ScoreAreaScaling scoreAreaScaling;

    // Skybox color change
    [Header("Skybox Color")]
    [SerializeField]
    private Camera mainCam;
    [SerializeField]
    private Camera cineCam;
    [SerializeField]
    private Color startColor;
    [SerializeField]
    private Color endColor;

    // Barrier speeds
    [Header("Barrier Speeds")]
    [SerializeField]
    private Renderer barrier;
    [SerializeField]
    private float startPulseSpeed;
    [SerializeField]
    private float endPulseSpeed;
    [SerializeField]
    private float startLineSpeed;
    [SerializeField]
    private float endLineSpeed;

    private void Start()
    {
        et = -delay;
        // set duraton
        mainMapScaling.duration = duration;
        endMapScaling.duration = duration;
        mainCameraFollow.duration = duration;
        cinemachineCameraFollow.duration = duration;
        barrierScript.duration = duration;
        scoreAreaScaling.duration = duration;

        //startColor = mainCam.backgroundColor;
    }

    private void Update()
    {
        et += Time.deltaTime;

        if (et >= 0.0f && et < duration)
        {
            mainMapScaling.et = et;
            endMapScaling.et = et;
            barrierScript.et = et;

            float value = et / duration;

            //pulse
            barrier.material.SetFloat("Vector1_887D380D", startPulseSpeed + (endPulseSpeed - startPulseSpeed) * value);
            //line
            barrier.material.SetFloat("Vector1_2564F6BE", startLineSpeed + (endLineSpeed - startLineSpeed) * value);

            Color bg = Color.Lerp(startColor, endColor, value);
            mainCam.backgroundColor = bg;
            cineCam.backgroundColor = bg;
        }

        mainCameraFollow.et = et;
        cinemachineCameraFollow.et = et;
        scoreAreaScaling.et = et;
    }
}
