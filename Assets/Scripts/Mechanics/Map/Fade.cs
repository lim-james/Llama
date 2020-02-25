using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class FadeData
{ 
    // time
    public float duration;
    public float delay;
    // colors
    public Color startColor;
    public Color targetColor;
}

public class Fade : MonoBehaviour
{
    // exposed attributes
    [SerializeField]
    private FadeData[] fades;
    [SerializeField]
    private int index;
    [SerializeField]
    private bool loop;

    // references
    private Image image;

    // private attributes
    private float t;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Start()
    {
        t = -fades[index].delay;
        image.color = fades[index].startColor;
    }

    private void Update()
    {
        t += Time.deltaTime;
        FadeData data = fades[index];

        if (t > data.duration)
        {
            if (index + 1 == fades.Length && !loop)
            {
                image.color = data.targetColor;
                return;
            }

            ++index;
            data = fades[index];
            t = -data.delay;
        }

        image.color = Color.Lerp(data.startColor, data.targetColor, Mathf.Max(t, 0.0f) / data.duration);
    }
}
