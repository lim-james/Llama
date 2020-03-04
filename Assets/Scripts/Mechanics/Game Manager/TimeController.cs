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
    [SerializeField]
    private UIController uiController;

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

    private bool paused;
    private bool playOnce;

    private AudioPlayer player;

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

        paused = false;
        player = GameObject.FindGameObjectWithTag("System").GetComponent<AudioPlayer>();
    }

    private void Update()
    {
        if (!paused)
        {
            et += Time.deltaTime;
            
            if(et >= -6.0 && !playOnce)
            {
                playOnce = true;
                player.PlayCountDown();
            }

            if (et >= 0.0f && et < duration)
            {
                mainMapScaling.et = et;
                endMapScaling.et = et;
                barrierScript.et = et;

                float value = et / duration;

                Color bg = Color.Lerp(startColor, endColor, value);
                mainCam.backgroundColor = bg;
                cineCam.backgroundColor = bg;

                value *= value;
                barrier.material.SetFloat("_et", et);
                barrier.material.SetFloat("_PulseSpeed", startPulseSpeed + (endPulseSpeed - startPulseSpeed) * value);
                barrier.material.SetFloat("_LineSpeed", startLineSpeed + (endLineSpeed - startLineSpeed) * value);

                // Audio pitching 
                player.BGNAudioPitching(0.0001f, 1.0f, 1.25f);
            }

            mainCameraFollow.et = et;
            cinemachineCameraFollow.et = et;
            scoreAreaScaling.et = et;
            uiController.et = et;
        }
    }

    public void TogglePaused()
    {
        if (paused == false)
            paused = true;
        else
            paused = false;
    }
}
