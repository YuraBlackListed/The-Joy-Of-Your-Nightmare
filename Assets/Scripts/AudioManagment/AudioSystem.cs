using System.Collections.Generic;
using UnityEngine;

public enum AudioType
{ 
    Music,
    Ambience,
    Monsters,
    Tools,
    Enviroment,
    Furniture
}
public enum AudioState
{ 
    Close,
    Open,
    None
}


public class AudioSystem : MonoBehaviour
{
    public static AudioSystem instance;

    [SerializeField] private List<AudioClipID> AudioClipsList;

    [SerializeField] private AudioClip ErrorSound;

    private static Dictionary<(AudioType, string), AudioSource> audioSources = new Dictionary<(AudioType, string), AudioSource>();
    private static Dictionary<(AudioType, string), AudioClip> audioClips = new Dictionary<(AudioType, string), AudioClip>();

    private void Awake()
    {
        instance = this;

        CreateAudioClipLibrary();

        CreateAudioSourceLibrary();
    }
    private void CreateAudioClipLibrary()
    {
        foreach (var clip in AudioClipsList)
        {
            string name = clip.ClipName;

            AudioType type = clip.ClipType;

            AudioClip Aclip = clip.Clip;

            audioClips.Add((type, name), Aclip);
        }
    }
    private void CreateAudioSourceLibrary()
    {
        AudioSource[] sources = FindObjectsOfType<AudioSource>();

        for (int i = 0; i < sources.Length; i++)
        {
            GameObject sourceGameObject = sources[i].gameObject;

            AudioSourceID ID = sourceGameObject.GetComponent<AudioSourceID>();

            audioSources.Add((ID.Type, ID.SourceName), sources[i]);
        }
    }

    #region ControlMethods
    public static void PlaySoundOnce(string name, AudioType type, AudioClip sound)
    {
        AudioSource source = audioSources[(type, name)];

        source.loop = false;

        source.clip = sound;

        source.Play();
    }
    public static void PlaySoundLooped(string name, AudioType type, AudioClip sound)
    {
        AudioSource source = audioSources[(type, name)];

        source.loop = true;

        source.clip = sound;

        source.Play();
    }
    public static void StopSound(string name, AudioType type, AudioClip sound)
    {
        AudioSource source = audioSources[(type, name)];

        source.Stop();
    }
    public static void PlaySetSoundAt(string name, AudioType type)
    {
        AudioSource source = audioSources[(type, name)];

        source.Play();
    }
    public static AudioClip GetSound(string clipName, AudioType type)
    {
        if(!audioClips.ContainsKey((type, clipName)))
        {
            return instance.ErrorSound;
        }

        AudioClip clip = audioClips[(type, clipName)];

        return clip;
    }
    #endregion
}
