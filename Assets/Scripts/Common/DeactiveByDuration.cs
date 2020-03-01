using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactiveByDuration : MonoBehaviour
{

    [SerializeField]
    private ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!ps.IsAlive())
            gameObject.SetActive(false);
    }
}
