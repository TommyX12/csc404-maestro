using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public List<Riff.Note> notes;
    public int BeatsPerBar;
    protected Riff riff;

    protected void Init() {
        riff = new Riff(BeatsPerBar, notes, MusicManager.Current);
    }

    public abstract void Fire();

    public virtual void OnBeat(int beatIdx) {
        Debug.Log("FIRE");
    }
}
