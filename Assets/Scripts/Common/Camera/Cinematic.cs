using System.Collections.Generic;
using UnityEngine;

/* Shots
 * Follow
 * -    Offset
 * -    Speed
 * -    
 * 
 */



public class Shot {
    public float duration;
    public List<Transform> targets;

}


public class Cinematic : MonoBehaviour
{

    public enum Mode {
        Follow,
        Stationary
    }

    [SerializeField]
    private List<Transform> targets;

}
