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

    public float duration { private get; set; }

    private bool activatedEndGame = false;
    private bool activateStartGame = false;

    public float et
    {
        set
        {
            if (value >= duration && !activatedEndGame)
            {
                GameObject.FindObjectOfType<EndGame>().StartEndGame();
                activatedEndGame = true;
            }

            if (value >= 0 && !activateStartGame)
            {
                CharacterInput[] characterMovements = GameObject.FindObjectsOfType<CharacterInput>();
                for (int i = 0; i < characterMovements.Length; ++i)
                {
                    characterMovements[i].moveable = true;
                }
                activateStartGame = true;
            }

            for (int i = 0; i < scoringArea.Count; ++i)
            {
                scoringArea[i].position = Vector3.Lerp(originalPositions[i], endPositions[i], value / duration);
            }
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i < scoringArea.Count; ++i)
        {
            Vector3 dir = (scoringArea[i].position - target.position).Sign();
            originalPositions.Add(scoringArea[i].position);
            endPositions.Add(target.position + Vector3.Scale(dir, minOffset));
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