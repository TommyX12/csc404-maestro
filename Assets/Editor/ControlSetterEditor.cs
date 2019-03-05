using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ControlSetter))]
[CanEditMultipleObjects]
public class ControlSetterEditor : Editor
{
    bool unsavedChanges = false;
    // previous things
    public bool[] grids;
    public bool[] sequences;
    public ControlSetter.ControlType type;

    private SerializedObject obj;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ControlSetter controls = (ControlSetter)target;
        if (controls.sequence == null || controls.gridSelection == null || controls.targetObjects.Count < 1) {
            return;
        }

        if (grids == null || sequences == null) {
            grids = controls.gridSelection.gridData;
            sequences = controls.sequence.data;
            type = controls.controlType;
            // load from target objects...
            // instead of other way around...
        }

        if (GUILayout.Button("Apply Changes")) {
            Undo.RecordObject(target, "Applied Changes");
            foreach (ControlSetter obj in targets)
            {
                obj.SetControls();
            }
            Util.SetCurrentSceneDirty();
        }
    }
}
