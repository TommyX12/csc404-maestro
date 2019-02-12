using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class SnapshotManager : MonoBehaviour
{

    public static SnapshotManager current;

    public List<AudioMixerSnapshot> snapshots;
    public List<AudioMixerSnapshot> defaultSnapshots;
    public AudioMixer master;
    private void Update()
    {
       
        if (snapshots.Count > 0)
        {
            float[] weights = new float[snapshots.Count];
            for (int i = 0; i < snapshots.Count; i++)
            {
                weights[i] = 1;
            }
            master.TransitionToSnapshots(snapshots.ToArray(), weights, 1f);
        }
        else {
            float[] weights = new float[defaultSnapshots.Count];
            for (int i = 0; i < defaultSnapshots.Count; i++)
            {
                weights[i] = 1;
            }
            master.TransitionToSnapshots(defaultSnapshots.ToArray(), weights, 1f);
        }
    }

    public void AddSnapshot(AudioMixerSnapshot snap) {
        snapshots.Add(snap);
    }

    public void RemoveSnapshot(AudioMixerSnapshot snap)
    {
        snapshots.Remove(snap);
    }

    private void Start()
    {
        current = this;
    }

}
