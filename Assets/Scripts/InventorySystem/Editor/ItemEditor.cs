using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Item))]
public class ItemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var item = target as Item;

        item.ItemName = EditorGUILayout.TextField("Item name", item.ItemName);
        item.Type = (ItemType)EditorGUILayout.EnumPopup("Item Type", item.Type);

        EditorGUILayout.Space(20f);


        EditorGUILayout.Space(20f);

        item.DoQuestLogic = GUILayout.Toggle(item.DoQuestLogic, "Do quest logic");

        if (item.DoQuestLogic)
        {
            item.IsPickable = EditorGUILayout.Toggle("Is pickable", item.IsPickable);
            item.QuestName = EditorGUILayout.TextField("Quest name", item.QuestName);
            item.ConditionName = EditorGUILayout.TextField("Condition name", item.ConditionName);
        }
    }
}
