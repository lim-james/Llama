using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageColorTransition : MonoBehaviour
{
    [SerializeField]
    private Color openedColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    [SerializeField]
    private Color closedColor = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    [SerializeField]
    private float duration = 1.0f;

    private Image image;

    private bool opened = false;
    private float timeLeft = 0.0f;

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

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Start()
    {
        timeLeft = duration;
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0) return;

        if (opened)
        {
            image.color = Color.Lerp(image.color, openedColor, Time.deltaTime / timeLeft);
        }
        else
        {
            image.color = Color.Lerp(image.color, closedColor, Time.deltaTime / timeLeft);
        }
    }
}

