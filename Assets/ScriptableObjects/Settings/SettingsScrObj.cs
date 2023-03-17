using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Scriptable Objects/Settings")]
public class SettingsScrObj : ScriptableObject
{
    public int sensitivity = 1;

    public int masterVolume = 0;
    public int otherVolume = 0;
    public int musicVolume = 0;

}
