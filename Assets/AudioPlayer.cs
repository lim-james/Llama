using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    private AudioSource source;
    
    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void Play(AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }

}
