using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    [SerializeField]
    private FruitSpawner fruitSpawner;
    
    public ScoreAreaScaling scoringAreas;
    public GameObject portalPrefab;
    public float respawnHeightOffset = 2.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterMovement>())
            ResetCharacter(other.gameObject);
        else if (other.GetComponent<Fruit>())
            ResetFruit(other.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Fruit>())
            ResetFruit(other.gameObject);
    }

    public void ResetCharacter(GameObject character)
    {
        character.GetComponent<CharacterInventory>().DiscardFruits();
        for (int i = 0; i < scoringAreas.scoringArea.Count; ++i)
        {
            if (scoringAreas.scoringArea[i].GetComponent<PlayerBase>().playerID != character.GetComponent<CharacterInfo>().playerID)
                continue;

            GameObject portal = Instantiate(portalPrefab);
            portal.transform.position = scoringAreas.scoringArea[i].transform.position + new Vector3(0, respawnHeightOffset, 0);
            character.transform.position = scoringAreas.scoringArea[i].transform.position + new Vector3(0, respawnHeightOffset, 0);
            break;
        }
    }

    public void ResetFruit(GameObject fruit)
    {
        fruitSpawner.ResetFruit(fruit);
    }
}
