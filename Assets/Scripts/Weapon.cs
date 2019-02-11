using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {
    public abstract Riff GetRiff();
    public abstract void Fire();
    public abstract void SetHost(Agent agent);
}
