using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class RangeHandler : UnityEvent<Transform> { }

[System.Serializable]
public class RangeState
{
    public LayerMask mask;
    public float range;
    public bool inRange;

    public RangeHandler enter;
    public RangeHandler stay;
    public RangeHandler exit;
}

public class RangeDetector : MonoBehaviour
{

    [SerializeField]
    private List<RangeState> states = new List<RangeState>();

    public bool active = true;

    // Update is called once per frame
    private void Update()
    {
        foreach (RangeState state in states)
        {
            Collider[] objectsNearBy = Physics.OverlapSphere(transform.position, state.range, state.mask);

            if (objectsNearBy.Length == 0)
            {
                state.exit.Invoke(null);
                continue;
            }

            foreach (Collider collider in objectsNearBy)
            {
                Transform target = collider.transform;

                float distance = (target.position - transform.position).magnitude;

                if (active && distance < state.range)
                {
                    // in range
                    if (state.inRange)
                    {
                        state.stay.Invoke(target);
                    }
                    else
                    {
                        state.inRange = true;
                        state.enter.Invoke(target);
                    }
                }
                else
                {
                    state.inRange = false;
                    state.exit.Invoke(target);
                }
            }
        }
    }
}
