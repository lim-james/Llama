using System.Collections.Generic;
using UnityEngine;

public class Shot {
    public enum Style {
        STICK, CHASE
    }

    public float duration;
    public Style style;

    public float movementSpeed;
    public float rotationSpeed;

    public List<Transform> targets;
    public Vector3 targetOffset;

    public Vector3 offset;
}


public class Cinematic : MonoBehaviour
{

    [SerializeField]
    private List<Shot> shots;
    [SerializeField]
    private bool loop;

    private Shot current;

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
    private float et;

    private void Start()
    {
        shotIndex = 0;
        et = 0.0f;
    }

    private void Update()
    {
        if (shots.Count == 0) return;

        Vector3 avgPosition = Vector3.zero;
        
        foreach (Transform target in current.targets)
            avgPosition += target.position;

        avgPosition /= current.targets.Count;

        Vector3 destination = avgPosition + current.targetOffset + current.offset;

        if (current.style == Shot.Style.STICK)
        {
            transform.position = destination;
        }
        else
        {
            float t = Time.deltaTime * current.movementSpeed;
            transform.position = Vector3.Lerp(transform.position, destination, t);
        }
        
        et += Time.deltaTime;
        if (current.duration >= 0 && et > current.duration)
            ++shotIndex;
    }


}
