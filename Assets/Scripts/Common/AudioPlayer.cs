using System.Collections;
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

    [SerializeField]
    private GameObject[] tempFeb;

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

    public void PlayBGM(int value, bool loop)
    {
        sources[1].clip = bgmClips[value];
        sources[1].loop = loop;
        sources[1].Play();
    }

    public void PlayCountDown(int value)
    {
        Instantiate(tempFeb[value]);
    }

    public IEnumerator FadeOut(float FadeTime)
    {
        float startVolume = sources[1].volume;

        while (sources[1].volume > 0)
        {
            sources[1].volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        // Reset portion
        sources[1].Stop();
        sources[1].volume = startVolume;
    }

    public IEnumerator FadeIn(float FadeTime)
    {
        sources[1].volume = 0.000f;
        
        while (sources[1].volume < 1f)
        {
            sources[1].volume += 1f * Time.deltaTime / FadeTime;
            
            yield return null;
        }

        // Reset portion
        sources[1].volume = 1f;
    }

    public void BGNAudioPitching(float value, float min, float max)
    {
        sources[1].pitch = Mathf.Clamp(value, min, max);
    }

    public void reset()
    {
        foreach (var i in sources)
        {
            i.Stop();
            i.pitch = 1;
        }
    }
}