using UnityEngine;

[CreateAssetMenu(fileName = "Audioclip ID", menuName = "Scriptable Objects/Audio")]
public class AudioClipID : ScriptableObject
{
    public AudioClip Clip;
    public string ClipName;
    public AudioType ClipType;
}
