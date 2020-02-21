using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorShift : MonoBehaviour
{
    public Color[] colors;

    public int currentIndex = 0;
    private int nextIndex;

    public float changeColorTime = 2.0f;
    //public float speed = 1;
    private float lastChange = 0.0f;
    private float timer = 0.0f;

    [SerializeField]
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        if (colors == null || colors.Length < 2)
            Debug.Log("Need to setup colors array in inspector");

        if (!image)
            image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > changeColorTime)
        {
            currentIndex = (currentIndex + 1) % colors.Length;
            nextIndex = (currentIndex + 1) % colors.Length;

            timer = 0.0f;
        }
        image.color = Color.Lerp(colors[currentIndex], colors[nextIndex], timer / changeColorTime);
    }
}
