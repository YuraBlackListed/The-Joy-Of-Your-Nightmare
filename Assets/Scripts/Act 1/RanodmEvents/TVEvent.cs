using UnityEngine;

public class TVEvent : RandomEvent
{
    [SerializeField] private GameObject lightSource;

    protected override void EventStart()
    {
        AudioClip clip = AudioSystem.GetSound("Static", AudioType.Effect);

        AudioSystem.PlaySoundOnce("Static", AudioType.Effect, clip);
        lightSource.SetActive(true);

        float length = Random.Range(1f, 10f);
        Invoke(nameof(EventEnd), length);
    }
    protected override void EventEnd()
    {
        AudioClip clip = AudioSystem.GetSound("Static", AudioType.Effect);

        AudioSystem.StopSound("Static", AudioType.Effect, clip);
        lightSource.SetActive(false);
    }
}
