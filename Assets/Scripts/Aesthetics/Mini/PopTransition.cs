using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopTransition : MonoBehaviour
{

    [SerializeField]
    private Vector3 openedScale = new Vector3(1.0f, 1.0f, 1.0f);
    [SerializeField]
    private Vector3 closedOffset = Vector3.zero;
    [SerializeField]
    private float duration = 1.0f;

    private bool opened = false;
    private float timeLeft = 0.0f;

    private Vector3 originalPosition;

    public bool isOpened
    {
        get
        {
            return opened;
        }
        set
        {
            timeLeft = duration;
            opened = value;
        }
    }

    private void Start()
    {
        timeLeft = duration;
        originalPosition = transform.localPosition;
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0) return;

        if (opened)
        {
            transform.localPosition   = Vector3.Lerp(transform.localPosition, originalPosition, Time.deltaTime / timeLeft);
            transform.localScale = Vector3.Lerp(transform.localScale, openedScale, Time.deltaTime / timeLeft);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition + closedOffset, Time.deltaTime / timeLeft);
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.deltaTime / timeLeft);
        }
    }
}
