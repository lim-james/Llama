using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioSource[] sources;

    [SerializeField]
    [Header("SFX")]
    private AudioClip[] sfxClips;

    [SerializeField]
    [Header("BGM")]
    private AudioClip[] bgmClips;

    private void Awake()
    {
        sources = GetComponents<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

    public void PlayHit()
    {
        sources[0].clip = sfxClips[4];
        sources[0].Play();
    }

    public void PlayThrow(int value)
    {
        sources[0].clip = sfxClips[value];
        sources[0].Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sources[0].clip = clip;
        sources[0].Play();
    }

    public void PlayBGM(int value)
    {
        sources[1].clip = bgmClips[value];
        sources[1].Play();
    }

    public void StopBGM()
    {
        sources[1].Stop();
    }
}