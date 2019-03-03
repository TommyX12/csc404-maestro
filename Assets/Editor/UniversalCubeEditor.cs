using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UniversalCube)), CanEditMultipleObjects]
public class UniversalCubeEditor : DeterministicObjectEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}
