using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crown : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed;
    [SerializeField]
    private float hoveringDistance;
    [SerializeField]
    private float minOffset, maxOffset;
    private float timer = 0.0f;
    public float originalHeightOffset = 0.0f;

    private bool up = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += (up) ? Time.fixedDeltaTime : -Time.fixedDeltaTime;
        timer = Mathf.Clamp01(timer);

        if (timer <= 0 || timer >= 1)
            up = !up;

        transform.Rotate(transform.up * Time.fixedDeltaTime * rotateSpeed);
        transform.localPosition = new Vector3(transform.localPosition.x, originalHeightOffset + Mathf.Lerp(minOffset, maxOffset, timer), transform.localPosition.z);
    }
}