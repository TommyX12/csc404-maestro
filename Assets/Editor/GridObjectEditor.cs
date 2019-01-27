using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridObject))]
[CanEditMultipleObjects]
public class GridObjectEditor : Editor
{
    private void PlaceInScene() {
        GridObject g = (GridObject)target;
        g.transform.localPosition = g.GetPosition();
        g.GridPos = g.GetGridPosition();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        PlaceInScene();
    }
}
