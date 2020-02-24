using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAreaScaling : MonoBehaviour
{
    public Transform target;
    public Vector3 minOffset;

    private List<Vector3> originalPositions = new List<Vector3>();
    private List<Vector3> endPositions = new List<Vector3>();
    public List<Transform> scoringArea = new List<Transform>();

    public float duration = 1.0f;
    public float delay = 0.0f;

    private float multipler = 0.0f;
    private float timer = 0.0f;
    public bool startTimer = false;

    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i < scoringArea.Count; ++i)
        {
            Vector3 dir = (scoringArea[i].position - target.position).Sign();
            originalPositions.Add(scoringArea[i].position);
            endPositions.Add(target.position + Vector3.Scale(dir, minOffset));
        }

        timer = -delay;
        //multipler = 1f / duration;
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!startTimer || timer < 0.0f)
            return;
        
        for (int i = 0; i < scoringArea.Count; ++i)
        {
            scoringArea[i].position = Vector3.Lerp(originalPositions[i], endPositions[i], timer / duration);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!target)
            return;

        for (int i = 0; i < scoringArea.Count; ++i)
        {
            Vector3 dir = (scoringArea[i].position - target.position).Sign();
            Gizmos.DrawLine(scoringArea[i].position, target.position + new Vector3(dir.x * minOffset.x, dir.y * minOffset.y, dir.z * minOffset.z));

            if (scoringArea[i].GetComponent<BoxCollider>())
                Gizmos.DrawWireCube(target.position + new Vector3(dir.x * minOffset.x, dir.y * minOffset.y, dir.z * minOffset.z), scoringArea[i].GetComponent<BoxCollider>().size);
        }
    }
}