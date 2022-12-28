using System.Collections.Generic;
using UnityEngine;

public enum AudioType
{ 
    Music,
    Ambience,
    Monsters,
    Tools
}

public class AudioSystem : MonoBehaviour
{
    public static AudioSystem instance;

    [SerializeField] private List<AudioClipID> AudioClipsList;

    private Dictionary<(AudioType, string), AudioSource> audioSources;
    private Dictionary<(AudioType, string), AudioClip> audioClips;

    private void Awake()
    {
        CreateAudioSourceLibrary();
        CreateAudioClipLibrary();

        instance = this;
    }
    private void CreateAudioClipLibrary()
    {
        for (int i = 0; i < AudioClipsList.Count; i++)
        {
            string clipName = AudioClipsList[i].ClipName;

            AudioType type = AudioClipsList[i].ClipType;

            AudioClip clip = AudioClipsList[i].Clip;

            audioClips.Add((type, clipName), clip);
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
        AudioSource source = instance.audioSources[(type, name)];

        source.loop = false;

        source.clip = sound;

        source.Play();
    }
    public static void PlaySoundLooped(string name, AudioType type, AudioClip sound)
    {
        AudioSource source = instance.audioSources[(type, name)];

        source.loop = true;

        source.clip = sound;

        source.Play();
    }
    public static void StopSound(string name, AudioType type, AudioClip sound)
    {
        AudioSource source = instance.audioSources[(type, name)];

        source.Stop();
    }
    public static AudioClip GetSound(string clipName, AudioType type)
    {
        return instance.audioClips[(type, clipName)];
    }
    #endregion
}
