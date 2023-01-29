using UnityEngine;
[CreateAssetMenu(fileName = "LevelScriptableObject", menuName = "Scriptable Objects/Level", order = 0)]
public class LevelScriptableObject : ScriptableObject 
{
    public int Night = 1;
    [Range(0.1f, 1)]public float EnemiesLevel;
}