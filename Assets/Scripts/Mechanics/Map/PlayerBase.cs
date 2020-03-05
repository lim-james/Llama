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
            if (_fruitCount == value)
                return;

            if (_fruitCount < value)
            {
                if (animator)
                    animator.SetTrigger("Add");
            }
            else
            {
                if (animator)
                    animator.SetTrigger("Minus");
            }

            _fruitCount = value;
            scoreLabel.text = _fruitCount.ToString();
        }
    }

    // references
    public GameObject animatorObj;
    private Text scoreLabel;
    private Animator animator;
    private string scoreObjectLayerName = "Fruit";

    private void Awake()
    {
        animator = animatorObj.GetComponent<Animator>();
        scoreLabel = GetComponentInChildren<Text>();
    }

    private void FixedUpdate()
    {
        Collider[] fruits = Physics.OverlapBox(transform.position, GetComponent<BoxCollider>().size /2.0f);

        int tempScore = 0;
        for (int i = 0; i < fruits.Length; ++i)
        {
            if (!fruits[i].GetComponent<Fruit>())
                continue;

            tempScore += fruits[i].GetComponent<Fruit>().stats.points;
        }

        fruitCount = tempScore;
    }
}