using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class ColliderHandler : MonoBehaviour
{
    [SerializeField]
    private UnityEvent OnEnter;

    void OnCollisionEnter(Collision collision)
    {
        if (OnEnter != null) OnEnter.Invoke();
    }
}
