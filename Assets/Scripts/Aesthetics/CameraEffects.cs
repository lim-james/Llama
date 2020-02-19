using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraEffects : MonoBehaviour
{
    private VolumeProfile volumeProfile;
    private LiftGammaGain liftGammaGain;
    private MotionBlur motionBlur;
    private Bloom bloom;
    private Vignette vignette;

    public float effectSpeed = 0.25f;

    public float gammaMaxIntensity = 0.5f;

    public float bloomMaxIntensity = 0.5f;
    
    public float vignetteMaxIntensity = 0.5f;

    public Color frenzyColor = new Color(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        volumeProfile = GetComponent<Volume>().profile;
        volumeProfile.TryGet(out liftGammaGain);
        volumeProfile.TryGet(out motionBlur);
        volumeProfile.TryGet(out bloom);
        volumeProfile.TryGet(out vignette);
    }

    private void Update()
    {
        
    }

    public void PlayFrenzyEffect()
    {
        vignette.color.value = frenzyColor;
        vignette.intensity.value = Mathf.Lerp(0.2f, 0.5f, Mathf.PingPong(Time.time, 1));    
    }

    public void StopFrenzyEffect()
    {
        vignette.color.value = new Color(0, 0, 0);
        vignette.intensity.value = 0;
    }

    // Get stun effect, Param duration > Time taken for the transition
    public void PlayStunEffect()
    {
        liftGammaGain.gamma.value = new Vector4(1, 1, 1, Mathf.PingPong(Time.time * effectSpeed, gammaMaxIntensity));
        bloom.intensity.value = Mathf.PingPong(Time.time * effectSpeed, bloomMaxIntensity);
        vignette.intensity.value = Mathf.PingPong(Time.time * effectSpeed, vignetteMaxIntensity);
    }

    public void StopStunEffect()
    {
        liftGammaGain.gain.value = new Vector4(1, 1, 1, 0);
        bloom.intensity.value = 0;
        vignette.intensity.value = 0;
    }
}
