using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class FearScript : MonoBehaviour
{
    [SerializeField] private LampScript lamp;
    [SerializeField] private FearDeath death;

    [SerializeField] private Volume volume;

    [SerializeField] private AnimationCurve curve;

    [SerializeField] private AudioMixer mixer;

    FilmGrain filmGrain;

    private float strength = 0.03f;

    private float deathStrength = 1.3f;


    private void Start()
    {
        volume.profile.TryGet<FilmGrain>(out filmGrain);
        filmGrain.intensity.Override(strength);
    }
    private void FixedUpdate()
    {
        if(strength >= deathStrength)
        {
            death.FallingAsleep();
        }
        Vector3 startPos = transform.position;
        float shakeStrength = curve.Evaluate(strength);
        transform.position = startPos + Random.insideUnitSphere * shakeStrength / 500;
        print(strength);
        if(lamp.active == false)
        {
            strength+=0.0005f;
            filmGrain.intensity.Override(strength);
            
            SetHeartbeatVolume(shakeStrength);
            SetOtherEffectStrenght(shakeStrength);
        }
    }
    private void SetHeartbeatVolume(float value)
    {
        mixer.SetFloat("HeartbeatVolume", Mathf.Log10(value) * 20);
    }
    private void SetOtherEffectStrenght(float value)
    {
        float value1 = 4400 / value;
        mixer.SetFloat("OtherEffectStrenght1", value1);


        float value2 = 10000 * (value - 1f);
        mixer.SetFloat("OtherEffectStrenght2", value2);
    }
    public void DecreaseFear(float decreasement)
    {
        strength -= decreasement;

        strength = Mathf.Clamp(strength, 0.03f, 1.5f);
    }
}