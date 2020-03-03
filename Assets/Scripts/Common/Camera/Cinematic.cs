using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Shot {
    public enum Style {
        STICK, CHASE
    }

    public float duration;
    public Style style;

    public float movementSpeed;
    public float zoomSpeed;

    public Transform target;
    public Vector3 targetOffset;
    public Vector3 offset;
    public float zoomOffset;
}

[ExecuteInEditMode]
public class Cinematic : MonoBehaviour
{

    [SerializeField]
    private List<Shot> shots;
    [SerializeField]
    private bool loop;

    private Shot current;
    private float zoom;

    private float et;
    private int _shotIndex;
    private int shotIndex
    {
        get
        {
            return _shotIndex;
        }
        set
        {
            _shotIndex = value;

            if (shotIndex >= shots.Count)
                shotIndex = 0;
            else if (shotIndex < 0)
                shotIndex = shots.Count - 1;

            et = 0.0f;
            current = shots[shotIndex];
        }
    }

    private void Start()
    {
        shotIndex = 0;
        et = 0.0f;
    }

    private void Update()
    {
        if (shots.Count == 0) return;
        UpdateCamera();
    }

    private void UpdateCamera()
    {
        Vector3 target = current.target.position;

        Vector3 focus = target + current.targetOffset;
        Vector3 direction = (focus - transform.position).normalized;

        if (current.style == Shot.Style.STICK)
        {
            zoom = current.zoomOffset;
            Vector3 destination = target + current.offset - direction * zoom;
            transform.position = destination;
            transform.LookAt(focus);
        }
        else
        {
            zoom += (current.zoomOffset - zoom) * Time.deltaTime * current.zoomSpeed;
            Vector3 destination = target + current.offset - direction * zoom;
            float mt = Time.deltaTime * current.movementSpeed;
            transform.position = Vector3.Lerp(transform.position, destination, mt);
            transform.LookAt(focus);
        }
        
        et += Time.deltaTime;
        if (current.duration >= 0 && et > current.duration)
            ++shotIndex;
    }

}
