using System.Collections.Generic;
using UnityEngine;

public enum AudioType
{ 
    Music,
    Ambience,
    Monsters,
    Tools,
    Enviroment,
    Furniture,
    Effect,
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

    private Dictionary<(AudioType, string), AudioSource> audioSources = new Dictionary<(AudioType, string), AudioSource>();
    private Dictionary<(AudioType, string), AudioClip> audioClips = new Dictionary<(AudioType, string), AudioClip>();

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
            if (sources[i].GetComponent<AudioSourceID>() != null)
            {
                GameObject sourceGameObject = sources[i].gameObject;
                if (!sourceGameObject.GetComponent<AudioSourceID>())
                {
                    return;
                }

                AudioSourceID ID = sourceGameObject.GetComponent<AudioSourceID>();

                audioSources.Add((ID.Type, ID.SourceName), sources[i]);
            }
        }
    }
    private void DoLogicAt(AudioSource source, float radius)
    {
        GameObject sourceGameobject = source.gameObject;

        SoundBait bait = sourceGameobject.GetComponent<SoundBait>();

        bait.SearchForMonster(radius);
    }

    #region ControlMethods
    public static void PlaySoundOnce(string name, AudioType type, AudioClip sound, bool doLogic = false, float radius = 0f)
    {
        AudioSource source = instance.audioSources[(type, name)];

        source.loop = false;

        source.clip = sound;

        source.Play();

        if (doLogic)
        {
            instance.DoLogicAt(source, radius);
        }
    }
    public static void PlaySoundLooped(string name, AudioType type, AudioClip sound, bool doLogic = false, float radius = 0f)
    {
        AudioSource source = instance.audioSources[(type, name)];

        source.loop = true;

        source.clip = sound;

        source.Play();

        if (doLogic)
        {
            instance.DoLogicAt(source, radius);
        }
    }
    public static void StopSound(string name, AudioType type, AudioClip sound)
    {
        AudioSource source = instance.audioSources[(type, name)];

        source.Stop();
    }
    public static void PlaySetSoundAt(string name, AudioType type, bool doLogic = false, float radius = 0f)
    {
        AudioSource source = instance.audioSources[(type, name)];

        source.Play();

        if (doLogic)
        {
            instance.DoLogicAt(source, radius);
        }
    }
    public static AudioClip GetSound(string clipName, AudioType type)
    {
        if(!instance.audioClips.ContainsKey((type, clipName)))
        {
            return instance.ErrorSound;
        }

        AudioClip clip = instance.audioClips[(type, clipName)];

        return clip;
    }
    #endregion
}
