using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DeterministicMenuUtility : MonoBehaviour
{
    [MenuItem("DeterministicObject/ApplyAllControllers")]
    static void ApplyAllControllers()
    {
        DeterministicObject[] objs = GameObject.FindObjectsOfType<DeterministicObject>();
        ControlSetter[] setters = GameObject.FindObjectsOfType<ControlSetter>();
        Undo.RecordObjects(objs, "ApplayControllers");
        foreach (DeterministicObject obj in objs) {
            obj.ClearControllers();
        }
        foreach (ControlSetter setter in setters) {
            setter.SetControls();
        }
        Util.SetCurrentSceneDirty();
    }
}
