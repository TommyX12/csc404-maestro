using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
// really only position need be serialized

[ExecuteInEditMode]
public class DeterministicObject : MonoBehaviour, TemporalObject
{
    // One Active component in charge of position
    // One Active component in charge of visuals
    // One Active or more components in charge of gameplay mechanic.

    // For universal block, these would have to be swappable on time.
    // will want a generic number (of tracks) later
    public List<TemporalPair> positionControllers;
    public List<TemporalPair> colorVisualization;
    public List<TemporalPair> shapeVisualization;

    [HideInInspector]
    public TemporalPair[] controlSequences;

    private int positionControllerPos = -1;
    private int colorVisualizationPos = -1;
    private int shapeVisualizationPos = -1;

    public TransformSerializationExtensions.TransformInfo startingTransform;

    // workaround for editor since I'm terribad at this
    private bool firstRun = true;

    // disgusting linear search
    // keep track of position in the list?
    public void Determine(float time)
    {
        positionControllerPos = DetermineHelper(positionControllers, time, positionControllerPos);
        colorVisualizationPos = DetermineHelper(colorVisualization, time, colorVisualizationPos);
        shapeVisualizationPos = DetermineHelper(shapeVisualization, time, shapeVisualizationPos);
    }

    // does the determination and returns new pos
    private int DetermineHelper(List<TemporalPair> pair, float time, int pos)
    {
        if (pair.Count < 1)
        {
            return -1;
        }

        if (time < pair[0].startTime) {
            return -1;
        }

        if (firstRun)
        {
            // linear search
            transform.LoadTransform(startingTransform);
            pair[0].controller.Initialize(this);
            firstRun = false;
        }

        // generate state from the beginning
        if (pos >= pair.Count || pos < 0 || time <= pair[pos].startTime)
        { // maybe just do -1 checking
            // linear search
            transform.LoadTransform(startingTransform);
            pair[0].controller.Initialize(this);
            int idx = 0;
            while (idx < pair.Count - 1 && time >= pair[idx + 1].startTime)
            {
                // fast forward gameObject state to what it would have been
                if (idx > 0)
                {
                    pair[idx - 1].controller.Determine(this, pair[idx].startTime - pair[idx - 1].startTime);
                    pair[idx].controller.Initialize(this);
                }
                idx++;
            }
            pos = idx;
        }

        // assuming pos is good.
        if (pos != pair.Count - 1 && time >= pair[pos + 1].startTime)
        {
            pos++;
            pair[pos - 1].controller.Determine(this, pair[pos].startTime - pair[pos - 1].startTime);
            pair[pos].controller.Initialize(this);
        }

        pair[pos].controller.Determine(this, time - pair[pos].startTime);
        return pos;
    }

    // serialize some start state
    public void SetStartState()
    {
        startingTransform = transform.StoreTransform();
    }

    public void Update()
    {
#if UNITY_EDITOR
        if (Scrubber.instance && Scrubber.instance.replay)
        {
            Determine(Scrubber.instance.source.time);
        }
        else
        {
            positionControllerPos = -1;
            colorVisualizationPos = -1;
            shapeVisualizationPos = -1;
        }
#endif
    }

    public void AddPositionController(TemporalController controller, float time)
    {
        InsertHelper(positionControllers, controller, time);
        positionControllerPos = -1;
    }

    public void AddVisualizationController(TemporalController controller, float time)
    {                     
        InsertHelper(colorVisualization, controller, time);
        colorVisualizationPos = -1;
    }

    public void AddShapeController(TemporalController controller, float time)
    {
        InsertHelper(shapeVisualization, controller, time);
        shapeVisualizationPos = -1;
    }

    public void RemovePositionController(float time) {
        RemoveHelper(positionControllers, time);
        positionControllerPos = -1;
    }

    public void RemoveVisualizationController(float time) {
        RemoveHelper(positionControllers, time);
        positionControllerPos = -1;
    }

    public void RemoveShapeController(float time) {
        RemoveHelper(positionControllers, time);
        positionControllerPos = -1;
    }

    public void ClearControllers() {
        positionControllers.Clear();
        shapeVisualization.Clear();
        colorVisualization.Clear();
        positionControllerPos = -1;
        colorVisualizationPos = -1;
        shapeVisualizationPos = -1;
    }

    private void InsertHelper(List<TemporalPair> list, TemporalController controller, float time)
    {
        TemporalPair pair = new TemporalPair { controller = controller, startTime = time };
        if (list.Count < 1 || time <= list[0].startTime)
        {
            list.Insert(0, pair);
        }
        else if (list[list.Count - 1].startTime <= time)
        {
            list.Add(pair);
        }
        else
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].startTime >= time) {
                    list.Insert(i, pair);
                    return;
                }
            }
        }
    }
    private void RemoveHelper(List<TemporalPair> list, float time) {
        for (int i = 0; i < list.Count; i++) {
            if (list[i].startTime - time <= 0.01) {
                list.RemoveAt(i);
                return;
            }
        }
    }
    /*
     public void SetActivePositionController(PositionController x);
     public void SetActiveVisualizer(Visualizer x);
    */

    private void OnEnable()
    {
#if UNITY_EDITOR
        EditorApplication.update += Update;
#endif
    }

    private void OnDisable()
    {
#if UNITY_EDITOR
        EditorApplication.update -= Update;
#endif
    }

}
