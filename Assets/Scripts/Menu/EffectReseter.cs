using UnityEngine;
using UnityEngine.Audio;

public class EffectReseter : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;

    private void Start() {
        SetOtherEffectStrenght(0f);
    }

    private void SetOtherEffectStrenght(float value)
    {
        float value1 = 4400 / value;
        mixer.SetFloat("OtherEffectStrenght1", value1);


        float value2 = 10000 * (value - 1f);
        mixer.SetFloat("OtherEffectStrenght2", value2);
    }
}
