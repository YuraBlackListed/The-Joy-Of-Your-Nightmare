using UnityEditor;
using UnityEngine.AI;


[CustomEditor(typeof(DoorScript))]
public class DoorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var door = target as DoorScript;

        door.MyObstacle = (NavMeshObstacle)EditorGUILayout.ObjectField(door.MyObstacle, typeof(NavMeshObstacle), true);

        door.IsOpened = EditorGUILayout.Toggle("Is opened", door.IsOpened);

        EditorGUILayout.Space(20f);

        door.OpenRotation = EditorGUILayout.Vector3Field("Open rotation", door.OpenRotation);
        door.ClosedRotation = EditorGUILayout.Vector3Field("Close rotation", door.ClosedRotation);

        door.CanOpen = EditorGUILayout.Toggle("Can open", door.CanOpen);

        EditorGUILayout.Space(20f);

        door.IsLocked = EditorGUILayout.Toggle("Is locked", door.IsLocked);

        if (door.IsLocked)
        {
            door.KeyName = EditorGUILayout.TextField("Key name", door.KeyName);
        }
    }
}
