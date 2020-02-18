using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NetworkObject))]
public class Interpolator : MonoBehaviour
{

    public float t;

    public Vector3 targetPosition;
    public Quaternion targetRotation;

    // Update is called once per frame
    void Update()
    {
        if (t > 0.0f)
        {
            float dt = Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, targetPosition, dt / t);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, dt / t);
            t -= dt;
        }
        else
        {
            //previousPosition = transform.position;
        }
    }
}
