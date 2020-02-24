using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    public ScoreAreaScaling scoringAreas;
    public GameObject portalPrefab;
    public float respawnHeightOffset = 2.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterMovement>())
            ResetCharacter(other.gameObject);
        else if (other.GetComponent<Fruit>())
            DestroyFruit(other.gameObject);
    }

    public void ResetCharacter(GameObject character)
    {
        for (int i = 0; i < scoringAreas.scoringArea.Count; ++i)
        {
            if (scoringAreas.scoringArea[i].GetComponent<PlayerBase>().playerID != character.GetComponent<CharacterInput>().controllerID)
                continue;

            GameObject portal = Instantiate(portalPrefab);
            portal.transform.position = scoringAreas.scoringArea[i].transform.position + new Vector3(0, respawnHeightOffset, 0);
            character.transform.position = scoringAreas.scoringArea[i].transform.position + new Vector3(0, respawnHeightOffset, 0);
            break;
        }

        //Trigger Inivisibility
    }

    public void DestroyFruit(GameObject fruit)
    {

        Destroy(fruit);
    }
}
