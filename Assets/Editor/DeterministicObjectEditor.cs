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
        if (GUILayout.Button("Set Starting State"))
        {
            foreach (DeterministicObject obj in targets)
            {
                obj.SetStartState();
            }
        }
    }
}
