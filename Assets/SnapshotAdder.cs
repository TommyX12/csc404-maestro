using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class SnapshotAdder : MonoBehaviour
{
    public AudioMixerSnapshot snapshot;


    public void AddSnapshot() {
        SnapshotManager.current.AddSnapshot(snapshot);
    }

    public void RemoveSnapshot() {
        SnapshotManager.current.RemoveSnapshot(snapshot);
    }
}
