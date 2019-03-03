using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [HideInInspector]
    public TransformSerializationExtensions.TransformInfo startingTransform;

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

        if (firstRun) {
            // linear search
            transform.LoadTransform(startingTransform);
            pair[0].controller.Initialize(gameObject);
            firstRun = false;
        }

        // generate state from the beginning
        if (pos >= pair.Count || pos < 0 || time <= pair[pos].startTime)
        { // maybe just do -1 checking
            // linear search
            transform.LoadTransform(startingTransform);
            pair[0].controller.Initialize(gameObject);
            int idx = 0;
            while (idx < pair.Count - 1 && time >= pair[idx + 1].startTime)
            {
                // fast forward gameObject state to what it would have been
                if (idx > 0)
                {
                    pair[idx - 1].controller.Determine(gameObject, pair[idx].startTime - pair[idx - 1].startTime);
                    pair[idx].controller.Initialize(gameObject);
                }
                idx++;
            }
            pos = idx;
        }

        // assuming pos is good.
        if (pos != pair.Count - 1 && time >= pair[pos + 1].startTime)
        {
            pos++;
            pair[pos - 1].controller.Determine(gameObject, pair[pos].startTime - pair[pos - 1].startTime);
            pair[pos].controller.Initialize(gameObject);
        }

        pair[pos].controller.Determine(gameObject, time - pair[pos].startTime);
        return pos;
    }

    // serialize some start state
    public void SetStartState() {
        startingTransform = transform.StoreTransform();
    }

    public void Update()
    {
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
    }

    /*
     public void SetActivePositionController(PositionController x);
     public void SetActiveVisualizer(Visualizer x);
    */
}
