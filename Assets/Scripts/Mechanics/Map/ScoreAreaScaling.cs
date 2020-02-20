﻿using System.Collections;
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
    private float multipler = 0.0f;
    private float timer = 0.0f;
    public bool startTimer = false;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < scoringArea.Count; ++i)
        {
            Vector3 dir = (scoringArea[i].position - target.position).normalized;
            originalPositions.Add(scoringArea[i].position);
            endPositions.Add(target.position + new Vector3(dir.x * minOffset.x, dir.y * minOffset.y, dir.z * minOffset.z));
        }

        multipler = 1 / duration;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!startTimer)
            return;

        timer += Time.deltaTime * multipler;
        for (int i = 0; i < scoringArea.Count; ++i)
        {
            scoringArea[i].position = Vector3.Lerp(originalPositions[i], endPositions[i], timer);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!target)
            return;

        for (int i = 0; i < scoringArea.Count; ++i)
        {
            Vector3 dir = (scoringArea[i].position - target.position).normalized;
            Gizmos.DrawLine(scoringArea[i].position, target.position + new Vector3(dir.x * minOffset.x, dir.y * minOffset.y, dir.z * minOffset.z));

            if (scoringArea[i].GetComponent<BoxCollider>())
                Gizmos.DrawWireCube(target.position + new Vector3(dir.x * minOffset.x, dir.y * minOffset.y, dir.z * minOffset.z), scoringArea[i].GetComponent<BoxCollider>().size);
        }
    }
}