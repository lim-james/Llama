using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class RangeHandler : UnityEvent<Transform> { }

[System.Serializable]
public class RangeState
{
    public Transform target;
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

    // Update is called once per frame
    private void Update()
    {
        foreach (RangeState state in states)
        {
            float distance = (state.target.position - transform.position).magnitude;
            
            if (distance < state.range)
            {
                // in range
                if (state.inRange)
                {
                    state.stay.Invoke(state.target);
                }
                else
                {
                    state.inRange = true;
                    state.enter.Invoke(state.target);
                }
            }
            else
            {
                state.inRange = false;
                state.exit.Invoke(state.target);
            }
        }
    }
}
