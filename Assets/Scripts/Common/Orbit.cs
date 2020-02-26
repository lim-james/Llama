using UnityEngine;

public class Orbit : MonoBehaviour
{

    [SerializeField]
    private Transform target;
    [SerializeField]
    private float radius = 10.0f;
    [SerializeField]
    private float speed = 10.0f;

    private void Update()
    {
        float dt = speed * Time.deltaTime;

        transform.RotateAround(target.position, Vector3.up, dt);
        var desiredPosition = (transform.position - target.position).normalized * radius + target.position;
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, dt);
    }
}
