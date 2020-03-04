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
    private bool playOnce = false;
    private bool playOnce2 = false;

    private AudioPlayer player;

    private float lerpTime = 1f;
    private float currentLerpTime;

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
        player.PlayBGM(2);
    }

    private void Update()
    {
        if (!paused)
        {
            et += Time.deltaTime;

            if(et >= -5.25f && !playOnce)
            {
                playOnce = true;
                player.PlayCountDown();
            }
            
            if (et >= 0.0f && et < duration)
            {
                if (!playOnce2)
                {
                    playOnce2 = true;
                    player.PlayBGM(1);
                }

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
                currentLerpTime += Time.deltaTime * 0.02f;
                if (currentLerpTime > lerpTime)
                    currentLerpTime = lerpTime;
                float t = currentLerpTime / lerpTime;
                t = 1f - Mathf.Cos(t * Mathf.PI * 0.5f);
                float final = Mathf.Lerp(1.0f, 1.25f, t);

                player.BGNAudioPitching(final, 1.0f, 1.25f);
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
