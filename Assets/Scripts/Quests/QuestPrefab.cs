using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest Template", menuName = "Scriptable Objects/Quests")]
public class QuestPrefab : ScriptableObject
{
    public string Title;

    [TextArea(10, 15)]
    public string Description;

    public QuestType Type;

    public List<string> ConditionNamesList;
}
