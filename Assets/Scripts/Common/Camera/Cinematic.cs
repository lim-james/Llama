using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Shot {
    public enum Style {
        STICK, CHASE, FIXED
    }

    public float duration;
    public Style style;

    public float movementSpeed;
    public float zoomSpeed;

    public Transform target;
    public Vector3 targetOffset;
    public Vector3 offset;
    public float zoomOffset;

    public bool followRotation;
}

[ExecuteInEditMode]
public class Cinematic : MonoBehaviour
{

    private float zoom;

    [SerializeField]
    private float et;
    [SerializeField]
    private int index;
    [SerializeField]
    private bool loop;

    public List<Shot> shots;

    private void Start()
    {
        et = 0.0f;

        for (int i = 1; i < Display.displays.Length; ++i)
        {
            Display.displays[i].Activate();
        }
    }

    private void Update()
    {
        if (shots.Count == 0) return;

        Shot current = null;

        if (index >= shots.Count)
        {
            if (loop)
            {
                index = 0;
                current = shots[index];
            }
            else
            {
                return;
            }
        }
        else
        {
            current = shots[index];
        }

        Transform target = current.target;

        Vector3 targetOffset = current.targetOffset;
        if (current.followRotation)
            targetOffset = target.forward * current.targetOffset.z + target.up * current.targetOffset.y + target.right * current.targetOffset.x;

        Vector3 focus = target.position + targetOffset;// current.targetOffset;
        Vector3 direction = (focus - transform.position).normalized;

        Vector3 offset = current.offset;
        if (current.followRotation)
            offset = target.forward * current.offset.z + target.up * current.offset.y + target.right * current.offset.x;

        if (current.style == Shot.Style.STICK)
        {
            zoom = current.zoomOffset;
            Vector3 destination = target.position + offset - direction * zoom;
            transform.position = destination;
            transform.LookAt(focus);
        }
        else if (current.style == Shot.Style.CHASE)
        {
            zoom += (current.zoomOffset - zoom) * Time.deltaTime * current.zoomSpeed;
            Vector3 destination = target.position + offset - direction * zoom;
            float mt = Time.deltaTime * current.movementSpeed;
            transform.position = Vector3.Lerp(transform.position, destination, mt);
            transform.LookAt(focus);
        }
        else if (current.style == Shot.Style.FIXED)
        {
            zoom += (current.zoomOffset - zoom) * Time.deltaTime * current.zoomSpeed;
            transform.position = offset - direction * zoom;
            transform.LookAt(focus);
        }
        
        et += Time.deltaTime;
        if (current.duration >= 0 && et > current.duration)
        {
            ++index;
            et = 0.0f;
            if (index >= shots.Count)
            {
                if (loop)
                {
                    index = 0;
                }
                else
                {
                    current = null;
                    return;
                }
            }
        }
    }

}
