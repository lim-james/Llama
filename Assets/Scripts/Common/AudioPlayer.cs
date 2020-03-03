using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioSource[] source;
    [SerializeField]
    private AudioClip hit;

    private void Awake()
    {
        source = GetComponents<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

    public void PlayHit()
    {
        source[0].clip = hit;
        source[0].Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        source[0].clip = clip;
        source[0].Play();
    }

    public void PlayBGM(AudioClip clip)
    {
        source[1].clip = clip;
        source[1].Play();
    }

    public void StopBGM()
    {
        source[1].Stop();
    }

}
