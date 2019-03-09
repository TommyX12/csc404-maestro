using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUp : TemporalController
{
    public float unitsUp = 1;
    public float lerpTime = 1;

    private Dictionary<DeterministicObject, Vector3> startingPositions = new Dictionary<DeterministicObject, Vector3>();

    public override void Determine(DeterministicObject obj, float time)
    {
        Vector3 start = startingPositions[obj];
        float t = Mathf.Min(1f, time / lerpTime);
        obj.transform.position = start + t * Vector3.up * unitsUp;
    }

    public override void Initialize(DeterministicObject obj)
    {
        startingPositions[obj] = obj.transform.position;
    }
}
