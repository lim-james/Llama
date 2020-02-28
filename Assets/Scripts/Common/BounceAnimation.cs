using UnityEngine;

public class BounceAnimation : MonoBehaviour
{
    // exposed attributes
    public float duration;
    public Vector3 scale;

    // private attributes
    private float speed;
    private bool animate;
    private bool large;

    private Vector3 originalScale;
    private float largeMagnitude;
    
    public void Start()
    {
        speed = 1.0f / duration;
        originalScale = transform.localScale;

        Vector3 largeScale = originalScale;
        largeScale.Scale(scale);
        largeMagnitude = largeScale.magnitude;
    }
    
    private void Update()
    {
        if (animate)
        {
            float dt = Time.deltaTime * speed;
            Vector3 dScale = new Vector3(dt, dt, dt);

            if (!large)
            {
                if (transform.localScale.magnitude < largeMagnitude)
                {
                    transform.localScale += dScale;
                }
                else
                {
                    large = true;
                }
            }
            else
            {
                if (transform.localScale.x > originalScale.x)
                {
                    transform.localScale -= dScale;
                }
                else
                {
                    animate = false;
                    large = false;
                }
            }
        }
    }

    // helper methods
    
    public void Animate()
    {
        animate = true;
        large = false;
    }
}
