using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteColorTransition : MonoBehaviour
{
    [SerializeField]
    private Color openedColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    [SerializeField]
    private Color closedColor = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    [SerializeField]
    private float duration = 1.0f;

    private SpriteRenderer sprite;

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
        sprite = GetComponent<SpriteRenderer>();
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
            sprite.color = Color.Lerp(sprite.color, openedColor, Time.deltaTime / timeLeft);
        }
        else
        {
            sprite.color = Color.Lerp(sprite.color, closedColor, Time.deltaTime / timeLeft);
        }
    }
}