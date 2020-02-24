using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBase : MonoBehaviour
{
    public int playerID;

    private int _fruitCount;
    public int fruitCount
    {
        get { return _fruitCount; }
        set
        {
            _fruitCount = value;
            scoreLabel.text = _fruitCount.ToString();
        }
    }

    // references
    private Text scoreLabel;
    private Animator animator;

    private void Awake()
    {
        scoreLabel = GetComponentInChildren<Text>();
        animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<Fruit>())
            return;

        fruitCount += other.GetComponent<Fruit>().stats.points;

        //animator.Play("ScoreAdd", 0);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.GetComponent<Fruit>())
            return;

        fruitCount -= other.GetComponent<Fruit>().stats.points;
    }
}
