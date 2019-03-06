using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface TemporalObject {
    void Determine(float time);
}

public abstract class TemporalController : MonoBehaviour {
    public abstract void Initialize(DeterministicObject obj);
    public abstract void Determine(DeterministicObject obj, float time);
}

[Serializable]
public struct TemporalPair {
    public TemporalController controller;
    public float startTime;
}