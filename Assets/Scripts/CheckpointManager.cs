using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{

    public static CheckpointManager instance;

    private void Awake()
    {
        instance = this;
    }

    private List<Checkpoint> Checkpoints = new List<Checkpoint>();

    private int active_checkpoint = -1;

    public int AddCheckpoint(Checkpoint checkpoint) {
        Checkpoints.Add(checkpoint);
        return Checkpoints.Count - 1;
    }

    public Checkpoint GetActiveCheckpoint() {
        if (active_checkpoint == -1) {
            Debug.LogWarning("HEY THERES NO DEFAULT CHECKPOINT");
            return null;
        }
        return Checkpoints[active_checkpoint];
    }

    public void SetActiveCheckpoint(int id)
    {
        if (id < 0 || id >= Checkpoints.Count) {
            Debug.LogWarning("Invalid checkpoint id");
            return;
        }

        for (int i = 0; i < Checkpoints.Count; i++) {
            if (i != id) {
                Checkpoints[i].UnMakeActiveCheckpoint();
            }
        }

        active_checkpoint = id;
    }
}
