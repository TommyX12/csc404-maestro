using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporalForwardMovement : TemporalController
{
    public float forwardSpeed;

    public Dictionary<DeterministicObject, Vector3> startPosition = new Dictionary<DeterministicObject, Vector3>();
    public Dictionary<DeterministicObject, Vector3> startForward = new Dictionary<DeterministicObject, Vector3>();

    public override void Determine(DeterministicObject obj, float time) {
        if (!startPosition.ContainsKey(obj)) {
            Initialize(obj);
        }

        obj.transform.position = startPosition[obj] + startForward[obj] * forwardSpeed * time;
    }

    public override void Initialize(DeterministicObject obj) {
        startPosition[obj] = obj.transform.position;
        startForward[obj] = obj.transform.forward;
    }
}