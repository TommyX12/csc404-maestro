using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DeterministicObject)), CanEditMultipleObjects]
public class DeterministicObjectEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Undo.RecordObjects(targets, "Set start state");
        if (GUILayout.Button("Set Starting State"))
        {
            foreach (DeterministicObject obj in targets)
            {
                obj.SetStartState();
                Util.SetCurrentSceneDirty();
                PrefabUtility.RecordPrefabInstancePropertyModifications(obj);
            }
        }

        Undo.RecordObjects(targets, "Clearing Controllers");
        if (GUILayout.Button("Clear controllers"))
        {
            foreach (DeterministicObject obj in targets)
            {
                obj.ClearControllers();
                Util.SetCurrentSceneDirty();
                PrefabUtility.RecordPrefabInstancePropertyModifications(obj);
            }
        }
        serializedObject.ApplyModifiedProperties();
    }
}
