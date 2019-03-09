using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class ControlSetter : MonoBehaviour
{
    public enum ControlType {
        POSITION,
        SHAPE,
        COLOR
    }
    public ControlType controlType = ControlType.COLOR;
    public TemporalController controller;
    public List<DeterministicObject> targetObjects;
    public GridSelector gridSelection;
    public Sequencer sequence;
    public void SetControls() {
#if UNITY_EDITOR
        if (Scrubber.instance == null) {
            Debug.LogWarning("Scrubber not set, no controls set");
            return;
        }
        for (int i = 0; i < sequence.data.Length; i++) {
            if (sequence.data[i]) {
                for (int j = 0; j < gridSelection.gridData.Length; j++) {
                    if (gridSelection.gridData[j]) {
                        float time = sequence.GetTime(i);
                        switch (controlType) {
                            case ControlType.POSITION:
                                targetObjects[j].AddPositionController(controller, time);
                                break;
                            case ControlType.SHAPE:
                                targetObjects[j].AddShapeController(controller, time);
                                break;
                            case ControlType.COLOR:
                                targetObjects[j].AddVisualizationController(controller, time);
                                break;
                        }
                        PrefabUtility.RecordPrefabInstancePropertyModifications(targetObjects[j]);
                    }
                }
            }
        }
#endif
    }
    public void RemoveControls(bool[] grids, bool[] sequences, ControlType controlType) {
        if (Scrubber.instance == null)
        {
            return;
        }
        for (int i = 0; i < sequence.data.Length; i++)
        {
            if (sequence.data[i])
            {
                for (int j = 0; j < gridSelection.gridData.Length; j++)
                {
                    if (gridSelection.gridData[j])
                    {
                        float time = sequence.GetTime(i);
                        switch (controlType)
                        {
                            case ControlType.POSITION:
                                targetObjects[j].RemovePositionController(time);
                                break;
                            case ControlType.SHAPE:
                                targetObjects[j].RemoveShapeController(time);
                                break;
                            case ControlType.COLOR:
                                targetObjects[j].RemoveVisualizationController(time);
                                break;
                        }
                    }
                }
            }
        }
    }
}
