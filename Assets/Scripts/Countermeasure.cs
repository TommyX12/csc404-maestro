using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Countermeasure : MonoBehaviour {
    public abstract void Fire();
    public abstract void SetTarget(Agent target);
    public abstract void SetHost(Agent agent);
    public abstract Riff GetTargetRiff();
}
