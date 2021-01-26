using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MovePlatform))]
public class MovePlatformEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MovePlatform script = (MovePlatform)target;

        if (GUILayout.Button("Create Path From Platform"))
        {
            if (script.path == null)
                script.path = new List<Vector3>();

            script.path.Add(new Vector3(script.transform.position.x, script.transform.position.y, script.transform.position.z));
        }

        if (GUILayout.Button("Delete Last Path Platform"))
        {
            if (script.path == null) return;

            if (script.path.Count > 0)
                script.path.RemoveAt(script.path.Count - 1);
        }

        if (GUILayout.Button("Clear all paths"))
        {
            script.path.Clear();
        }

    }
}