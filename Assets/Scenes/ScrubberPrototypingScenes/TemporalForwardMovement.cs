using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporalForwardMovement : TemporalController
{
    public float forwardSpeed;

    public Dictionary<GameObject, Vector3> startPosition = new Dictionary<GameObject, Vector3>();
    public Dictionary<GameObject, Vector3> startForward = new Dictionary<GameObject, Vector3>();

    public override void Determine(GameObject obj, float time) {
        if (!startPosition.ContainsKey(obj)) {
            Initialize(obj);
        }

        obj.transform.position = startPosition[obj] + startForward[obj] * forwardSpeed * time;
    }

    public override void Initialize(GameObject obj) {
        startPosition[obj] = obj.transform.position;
        startForward[obj] = obj.transform.forward;
    }
}