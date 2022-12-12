using System.Collections.Generic;
using UnityEngine;

public enum AudioType
{ 
    Music,
    Ambience,
    Monsters
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
    }
    private void Start()
    {
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
    public void PlaySoundOnce(string name, AudioType type, AudioClip sound)
    {
        AudioSource source = audioSources[(type, name)];

        source.loop = false;

        source.clip = sound;

        source.Play();
    }
    public void PlaySoundLooped(string name, AudioType type, AudioClip sound)
    {
        AudioSource source = audioSources[(type, name)];

        source.loop = true;

        source.clip = sound;

        source.Play();
    }
    public void StopSound(string name, AudioType type, AudioClip sound)
    {
        AudioSource source = audioSources[(type, name)];

        source.Stop();
    }
    public AudioClip GetSound(string clipName, AudioType type)
    {
        return audioClips[(type, clipName)];
    }
    #endregion
}
