using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Note Template", menuName = "Scriptable Objects/Notes")]
[Serializable]
public class NotePrefab : ScriptableObject
{
    public string NoteName;

    [TextArea(10, 20)]
    public string NoteText;
}
