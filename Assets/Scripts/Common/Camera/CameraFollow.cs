using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraFollow : MonoBehaviour
{
    public enum FollowStyle
    {
        SNAP_FOLLOW,
        SMOOTH_FOLLOW
    }

    [SerializeField]
    private Camera followCamera;
    public Rigidbody target;
    public float startDistance;
    public float endDistance;
    public float heightOffset;
    public Vector3 rotation;
    public FollowStyle style;


    [SerializeField]
    private float duration;
    [SerializeField]
    private float delay;
    [SerializeField]
    private float time;

    private float magnitude;
    [SerializeField]
    private float distance;

    [SerializeField]
    private float followMultipler = 1.0f;
    private float followTimer = 1.0f;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        if (!followCamera)
        {
            followCamera = GetComponent<Camera>();
            startPos = followCamera.transform.position;
        }

        time = -delay;
        magnitude = 1.0f / (startDistance - endDistance);
    }

    void Update()
    {
        if (!target)
            return;

        time += Time.deltaTime;

        if (time < 0.0f)
            distance = startDistance;
        else if (time < duration)
            distance = startDistance + (endDistance - startDistance) * time / duration;
        else
            distance = endDistance;

        MoveCamera();
    }

    void FixedUpdate()
    {
        if (!target)
            return;

        MoveCamera();
    }

    void MoveCamera()
    {
        transform.localEulerAngles = rotation;

        switch (style)
        {
            case FollowStyle.SNAP_FOLLOW:
                followCamera.transform.position = target.position + new Vector3(0, heightOffset, 0) + Quaternion.Euler(rotation) * Vector3.back * distance;
                break;
            case FollowStyle.SMOOTH_FOLLOW:
                if (target.velocity.magnitude > 0)
                {
                    startPos = followCamera.transform.position;
                    followTimer = Mathf.Clamp01(Time.deltaTime * followMultipler);
                }
                else
                {
                    followTimer = Mathf.Clamp01(followTimer + Time.deltaTime * followMultipler);
                }
                followCamera.transform.position = Vector3.Lerp(startPos, target.position + new Vector3(0, heightOffset, 0) + Quaternion.Euler(rotation) * Vector3.back * distance, followTimer);
                break;
        }
    }
}